const API_BASE = "https://localhost:7170/api";
const AUTH_TOKEN_KEY = "jwtToken";
const AUTH_KEY = "isAuthenticated";
const CONTENT_TYPES = ["profile", "project", "skill", "experience"];
const crudForm = document.getElementById("crudForm");
const contentTypeSelect = document.getElementById("contentType");
const dynamicFieldsContainer = document.getElementById("dynamicFields");
const contentTableBody = document.querySelector("#contentTable tbody");
const crudMessage = document.getElementById("crudMessage");
const emptyListMessage = document.getElementById("emptyListMessage");
const submitBtn = document.getElementById("submitBtn");
const logoutBtn = document.getElementById("logoutBtn");
const clearFormBtn = document.getElementById("clearFormBtn");
const userModal = document.getElementById("userModal");
const openBtn = document.getElementById("userProfileBtn");
const closeBtn = document.getElementById("closeModal");
const userForm = document.getElementById("userForm");
const userMessage = document.getElementById("userMessage");

let currentUserId = null;


async function apiFetch(url, method = "GET", data = null) {
    const options = {
        method,
        headers: { "Content-Type": "application/json" },
    };
    if (data) options.body = JSON.stringify(data);

    const response = await fetch(url, options);
    let result = null;

    try {
        const text = await response.text();
        result = text ? JSON.parse(text) : null;
    } catch (err) {
        result = null;
    }

    if (!response.ok) {
        const errorMessage = result?.message || JSON.stringify(result) || `HTTP ${response.status}`;
        throw new Error(errorMessage);
    }

    return result;
}

const userApi = {
    async getAllUsers() {
        try {
            const users = await apiFetchWithAuth(`${API_BASE}/User/GetAll`, "GET");
            return users || [];
        } catch (err) {
            showUserMessage(`❌ Kullanıcı listesi alınamadı: ${err.message}`, "error");
            console.error(err);
            return [];
        }
    },

    async updateUser(user) {
        try {
            const token = localStorage.getItem(AUTH_TOKEN_KEY);
            const response = await fetch(`${API_BASE}/User/Update`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": token ? `Bearer ${token}` : ""
                },
                body: JSON.stringify(user)
            });

            let data;
            try {
                data = await response.json();
            } catch {
                data = {};
            }

            if (!response.ok) {
                if (data.errors) {
                    showValidationErrors(data.errors);
                    showUserMessage("⚠️ Formda hatalar var, lütfen düzeltin.", "error");
                    return false;
                }

                const errorMessage =
                    data.message ||
                    data.Message ||
                    data.description ||
                    "⚠️ Güncelleme başarısız. Lütfen tekrar deneyin.";

                showUserMessage(errorMessage, "error");
                return false;
            }

            showUserMessage(data.message || "✅ Bilgileriniz başarıyla güncellendi", "success");
            document.querySelectorAll(".field-error").forEach(e => e.remove());
            return true;

        } catch (err) {
            showUserMessage("⚠️ Sunucuya bağlanılamadı veya beklenmeyen bir hata oluştu", "error");
            return false;
        }
    }




};


openBtn?.addEventListener("click", async () => {
    userModal.classList.add("active");

    const users = await userApi.getAllUsers();
    if (!users || users.length === 0) {
        showUserMessage("⚠️ Hiç kullanıcı bulunamadı.", "error");
        return;
    }

    const user = users[0];
    currentUserId = user.id || user.Id;

    document.getElementById("name").value = user.name ?? "";
    document.getElementById("lastName").value = user.lastName ?? "";
    document.getElementById("userName").value = user.userName ?? "";
    document.getElementById("email").value = user.email ?? "";
    document.getElementById("phoneNumber").value = user.phoneNumber ?? "";
    document.getElementById("currentPassword").value = "";
    document.getElementById("newPassword").value = "";
});

async function apiFetchWithAuth(path, method = "GET", body = null) {
    const token = localStorage.getItem(AUTH_TOKEN_KEY);
    const headers = { "Accept": "application/json" };
    if (!(body instanceof FormData)) headers["Content-Type"] = "application/json";
    if (token) headers["Authorization"] = `Bearer ${token}`;
    const opts = { method, headers };
    if (body) opts.body = (body instanceof FormData) ? body : JSON.stringify(body);
    const res = await fetch(path, opts);

    if (res.status === 204) return null;
    let payload = null;
    const text = await res.text();
    try { payload = text ? JSON.parse(text) : null; } catch { payload = text; }
    if (!res.ok) {
        const msg = (payload && (payload.message || payload.Message || payload)) || res.statusText;
        const err = new Error(msg);
        err.status = res.status;
        throw err;
    }
    return payload;
}


userForm?.addEventListener("submit", async (e) => {
    e.preventDefault();

    const user = {
        Id: currentUserId,
        Name: document.getElementById("name").value.trim(),
        LastName: document.getElementById("lastName").value.trim(),
        UserName: document.getElementById("userName").value.trim(),
        Email: document.getElementById("email").value.trim(),
        PhoneNumber: document.getElementById("phoneNumber").value.trim(),
        CurrentPassword: document.getElementById("currentPassword").value.trim(),
        NewPassword: document.getElementById("newPassword").value.trim(),
    };

    if (!user.Name || !user.Email || !user.UserName) {
        showUserMessage("⚠️ Lütfen zorunlu alanları doldurun.", "error");
        return;
    }

    const success = await userApi.updateUser(user);

    if (!success) {
        return;
    }


    showUserMessage("✅ Bilgiler başarıyla güncellendi.", "success");
    setTimeout(() => closeUserModal(), 5000);
});

document.addEventListener("click", (e) => {
    if (e.target.classList.contains("toggle-password")) {
        const targetId = e.target.dataset.target;
        const input = document.getElementById(targetId);
        if (!input) return;

        const isVisible = input.type === "text";
        input.type = isVisible ? "password" : "text";

        e.target.classList.toggle("fa-eye");
        e.target.classList.toggle("fa-eye-slash");
    }
});


closeBtn?.addEventListener("click", closeUserModal);
function closeUserModal() {
    userModal.classList.remove("active");
    userForm.reset();
    userMessage.textContent = "";
}

function showUserMessage(message, type) {
    const msgBox = document.getElementById("userMessage");
    msgBox.textContent = message;
    msgBox.className = type === "error" ? "text-red-500" : "text-green-500";
    msgBox.style.display = "block";
}




function showValidationErrors(errors) {

    document.querySelectorAll(".field-error").forEach(e => e.remove());

    for (const key in errors) {
        const input = document.getElementById(key);
        if (input) {
            const errorDiv = document.createElement("div");
            errorDiv.className = "field-error text-red-500 text-sm mt-1";
            errorDiv.textContent = errors[key][0];
            input.insertAdjacentElement("afterend", errorDiv);
        }
    }
}














const fieldTemplates = {
    profile: [
        { id: "FullName", label: "Ad Soyad", type: "text" },
        { id: "Title", label: "Ünvan", type: "text" },
        { id: "Biography", label: "Biyografi", type: "textarea" },
        { id: "Email", label: "E-posta", type: "email" },
        { id: "GithubUrl", label: "Github URL", type: "text" },
        { id: "LinkedInUrl", label: "LinkedIn URL", type: "text" }
    ],
    project: [
        { id: "Title", label: "Proje Başlığı", type: "text" },
        { id: "Description", label: "Açıklama", type: "textarea" },
        { id: "ImageUrl", label: "Görsel URL", type: "text" },
        { id: "GithubUrl", label: "Github URL", type: "text" },
        { id: "Technologies", label: "Teknolojiler", type: "text" }
    ],
    skill: [
        { id: "Name", label: "Yetenek Adı", type: "text" },
        { id: "Level", label: "Seviye (1-100)", type: "number" },
        { id: "IconClass", label: "İkon Sınıfı", type: "text" },
        { id: "Category", label: "Kategorisi", type: "text" }
    ],
    experience: [
        { id: "CompanyName", label: "Şirket Adı", type: "text" },
        { id: "Position", label: "Pozisyonunuz", type: "text" },
        { id: "Sector", label: "Sektör", type: "text" },
        { id: "StartDate", label: "Başlama Tarihi", type: "date" },
        { id: "EndDate", label: "Bitiş Tarihi", type: "date" },
        { id: "WorkingMethod", label: "Çalışma Şekli (Uzaktan, Ofis, Hibrit)", type: "text" },
        { id: "Explanation", label: "Açıklama", type: "textarea" },
        { id: "TechnologyUsed", label: "Kullandığınız Teknolojiler (virgülle ayırın)", type: "text" }
    ]

};


async function apiFetch(path, method = "GET", body = null) {
    const token = localStorage.getItem(AUTH_TOKEN_KEY);
    const headers = { "Accept": "application/json" };
    if (!(body instanceof FormData)) headers["Content-Type"] = "application/json";
    if (token) headers["Authorization"] = `Bearer ${token}`;

    const opts = { method, headers };
    if (body) opts.body = (body instanceof FormData) ? body : JSON.stringify(body);

    const res = await fetch(`${API_BASE}${path}`, opts);

    if (res.status === 204) return null;


    let payload = null;
    const text = await res.text();
    try { payload = text ? JSON.parse(text) : null; } catch { payload = text; }

    if (!res.ok) {

        const msg = (payload && (payload.message || payload.Message || payload)) || res.statusText;
        const err = new Error(msg);
        err.status = res.status;
        throw err;
    }
    return payload;
}

function checkAdminAccess() {
    const token = localStorage.getItem(AUTH_TOKEN_KEY);
    if (!token) {
        alert("Giriş yapmanız gerekiyor.");
        window.location.href = "login.html";
    }
}

function setupLogout() {
    logoutBtn?.addEventListener("click", () => {
        localStorage.removeItem(AUTH_TOKEN_KEY);
        localStorage.removeItem(AUTH_KEY);
        window.location.href = "login.html";
    });
}


function renderDynamicFields(type) {
    dynamicFieldsContainer.innerHTML = "";
    const template = fieldTemplates[type];
    if (!template) return;

    template.forEach(({ id, label, type }) => {
        const wrapper = document.createElement("div");
        wrapper.className = "form-group";

        const labelEl = document.createElement("label");
        labelEl.setAttribute("for", id);

        const input = (type === "textarea") ? document.createElement("textarea") : document.createElement("input");
        if (type !== "textarea") input.type = type;
        input.id = id;
        input.name = id;
        input.placeholder = label;
        input.required = true;

        wrapper.append(labelEl, input);
        dynamicFieldsContainer.appendChild(wrapper);
    });
}

async function loadListForType(type) {
    try {
        const ctrl = capitalize(type);
        const path = `/${ctrl}/GetAll`;
        const items = await apiFetch(path, "GET");

        renderListRows(type, items || []);
    } catch (err) {
        showCrudMessage("Liste yüklenemedi: " + (err.message || err), "error");
        contentTableBody.innerHTML = "";
        emptyListMessage.style.display = "block";
    }
}

function renderListRows(type, items) {
    contentTableBody.innerHTML = "";

    if (!items || items.length === 0) {
        emptyListMessage.style.display = "block";
        return;
    }
    emptyListMessage.style.display = "none";

    items.forEach(item => {

        const id = item.id ?? item.Id ?? item.ID;
        const title = item.Title ?? item.title ?? item.FullName ?? item.fullName ?? item.Name ?? item.name ?? "(Başlıksız)";


        const row = contentTableBody.insertRow();
        row.innerHTML = `
            <td>${id}</td>
            <td>${type}</td>
            <td>${String(title)}</td>
            <td>
                <button class="action-btn edit-btn" data-id="${id}" data-type="${type}">
                    <i class="fas fa-edit"></i> Düzenle
                </button>
                <button class="action-btn delete-btn" data-id="${id}" data-type="${type}">
                    <i class="fas fa-trash"></i> Sil
                </button>
            </td>
        `;
    });

    attachActionListeners();
}

async function deleteItemApi(id, type) {
    if (!confirm("Bu içeriği kalıcı olarak silmek istediğinizden emin misiniz?")) return;
    try {
        const ctrl = capitalize(type);
        await apiFetch(`/${ctrl}/RemoveById`, "POST", { Id: id });
        showCrudMessage("Kayıt başarıyla silindi.", "success");
        await loadListForType(type);
        clearForm();
    } catch (err) {
        showCrudMessage("Silme hatası: " + (err.message || err), "error");
    }
}
async function submitItemApi(isUpdate = false) {
    const type = contentTypeSelect.value;
    if (!type) return alert("Lütfen içerik tipi seçiniz.");

    const template = fieldTemplates[type];
    const body = {};

    template.forEach(f => {
        const el = document.getElementById(f.id);
        body[f.id] = el ? el.value.trim() : "";
    });

    const existingId = document.getElementById("contentId").value;
    if (isUpdate && !existingId) {
        return showCrudMessage("Güncelleme için Id bulunamadı.", "error");
    }

    const ctrl = capitalize(type);
    const endpoint = isUpdate ? `/${ctrl}/Update` : `/${ctrl}/Add`;
    const payload = isUpdate ? { Id: existingId, ...body } : body;

    try {
        await apiFetch(endpoint, "POST", payload);
        showCrudMessage(isUpdate ? "Kayıt güncellendi." : "Yeni içerik eklendi.", "success");
        await loadListForType(type);
        clearForm();
    } catch (err) {
        showCrudMessage("Kaydetme hatası: " + (err.message || err), "error");
    }
}


async function editItemApi(id, type) {
    try {
        const ctrl = capitalize(type);
        const items = await apiFetch(`/${ctrl}/GetAll`, "GET");
        const item = (items || []).find(i => String(i.id ?? i.Id) === String(id));
        if (!item) throw new Error("Öğe bulunamadı.");

        document.getElementById("contentId").value = item.id ?? item.Id ?? item.ID;
        contentTypeSelect.value = type;
        renderDynamicFields(type);

        const template = fieldTemplates[type];
        setTimeout(() => {
            template.forEach(f => {
                const el = document.getElementById(f.id);
                if (el) el.value = item[f.id] ?? item[f.id.charAt(0).toLowerCase() + f.id.slice(1)] ?? "";
            });
        }, 0);

        showCrudMessage("Düzenleme moduna geçildi.", "info");
        window.scrollTo({ top: 0, behavior: "smooth" });
    } catch (err) {
        showCrudMessage("Düzenleme hatası: " + (err.message || err), "error");
    }
}


function attachActionListeners() {
    document.querySelectorAll(".edit-btn").forEach(btn => {
        btn.onclick = () => editItemApi(btn.dataset.id, btn.dataset.type);
    });
    document.querySelectorAll(".delete-btn").forEach(btn => {
        btn.onclick = () => deleteItemApi(btn.dataset.id, btn.dataset.type);
    });
}


async function handleFormSubmit(e) {
    e.preventDefault();
    const existingId = document.getElementById("contentId").value;
    if (existingId) await submitItemApi(true);
    else await submitItemApi(false);
}


function capitalize(s) {
    if (!s) return s;
    return s.charAt(0).toUpperCase() + s.slice(1);
}

function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function clearForm() {
    crudForm.reset();
    dynamicFieldsContainer.innerHTML = "";
    document.getElementById("contentId").value = "";
    submitBtn.querySelector("span").textContent = "Ekle / Güncelle";
}


function showCrudMessage(message, type = "info") {
    crudMessage.textContent = message;
    crudMessage.className = `auth-message ${type}-message`;
    setTimeout(() => (crudMessage.textContent = ""), 3500);
}


document.addEventListener("DOMContentLoaded", async () => {
    checkAdminAccess();
    setupLogout();


    contentTypeSelect.addEventListener("change", async (e) => {
        const type = e.target.value;
        renderDynamicFields(type);
        if (type) await loadListForType(type);
    });

    crudForm.addEventListener("submit", handleFormSubmit);
    clearFormBtn?.addEventListener("click", clearForm);


    const initialType = contentTypeSelect.value;
    if (initialType) {
        renderDynamicFields(initialType);
        await loadListForType(initialType);
    }
});






openBtn?.addEventListener("click", () => userModal.classList.add("active"));
closeBtn?.addEventListener("click", closeUserModal);
window.addEventListener("click", (e) => {
    if (e.target === userModal) closeUserModal();
});


document.querySelectorAll(".toggle-password").forEach(icon => {
    icon.addEventListener("click", () => {
        const targetId = icon.dataset.target;
        const input = document.getElementById(targetId);
        const isVisible = input.type === "text";
        input.type = isVisible ? "password" : "text";
        icon.classList.toggle("fa-eye");
        icon.classList.toggle("fa-eye-slash");
    });
});


userForm?.addEventListener("submit", (e) => {
    e.preventDefault();
    showUserMessage("✅ Bilgiler başarıyla kaydedildi (örnek mod).", "success");
    setTimeout(() => closeUserModal(), 1200);
});

function showUserMessage(msg, type = "info") {
    userMessage.textContent = msg;
    userMessage.className = `auth-message ${type}-message`;
    userMessage.style.opacity = "1";
    setTimeout(() => {
        userMessage.style.opacity = "0";
    }, 3500);
}

function closeUserModal() {
    userModal.classList.remove("active");
    setTimeout(() => {
        userForm.reset();
        userMessage.textContent = "";
        userMessage.style.opacity = "0";
    }, 300);
}

document.addEventListener("click", (e) => {
    if (e.target.classList.contains("toggle-password")) {
        const targetId = e.target.dataset.target;
        const input = document.getElementById(targetId);
        if (!input) return;

        const isVisible = input.type === "text";
        input.type = isVisible ? "password" : "text";

        e.target.classList.toggle("fa-eye");
        e.target.classList.toggle("fa-eye-slash");
    }
});