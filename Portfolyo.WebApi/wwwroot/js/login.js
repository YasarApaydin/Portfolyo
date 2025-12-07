document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById("loginForm");
    const loginMessage = document.getElementById("loginMessage");
    const togglePassword = document.getElementById("togglePassword");
    const passwordInput = document.getElementById("password");


    if (togglePassword && passwordInput) {
        togglePassword.addEventListener("click", () => {
            const type = passwordInput.getAttribute("type") === "password" ? "text" : "password";
            passwordInput.setAttribute("type", type);

            // ikon değişimi
            togglePassword.classList.toggle("fa-eye");
            togglePassword.classList.toggle("fa-eye-slash");
        });
    }
    if (loginForm) {
        loginForm.addEventListener('submit', async function (e) {
            e.preventDefault();

            const userName = document.getElementById("username").value;
            const password = document.getElementById("password").value;

            loginMessage.className = "auth-message";
            loginMessage.textContent = "Giriş Yapılıyor...";

            const response = await fetch("https://yasarapaydinportfolyo-a5e0bwb5e8hrede5.westeurope-01.azurewebsites.net/api/auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ userNameOrEmail: userName, password: password })
            });

            if (!response.ok) {
                const err = await response.json();
                loginMessage.textContent = err.title || "Hatalı giriş!";
                loginMessage.classList.add("error-message");
                return;
            }

            const data = await response.json();

            localStorage.setItem("jwtToken", data.accessToken);

            loginMessage.textContent = "Giriş başarılı! Yönlendiriliyor...";
            loginMessage.classList.add("success-message");

            setTimeout(() => {
                window.location.href = "/admin.html";
            }, 1000);
        });
    }


});
