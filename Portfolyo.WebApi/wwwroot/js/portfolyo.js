const API_BASE_URL = "https://yasarapaydinportfolyo-a5e0bwb5e8hrede5.westeurope-01.azurewebsites.net";

const GITHUB_USERNAME = "YasarApaydin";
document.addEventListener("DOMContentLoaded", async () => {
    try {
        // --- 1️⃣ PROFİL ---
        const profileRes = await fetch(`${API_BASE_URL}/Profile/GetAll`);
        const profiles = await profileRes.json();

        if (profiles?.length > 0) {
            const p = profiles[0];

            document.querySelector(".name-text").textContent = p.fullName;
            document.querySelector(".title-gradient").textContent = p.title;
            document.querySelector(".hero-description").textContent = p.biography;

            document.querySelectorAll(".github").forEach(el => el.href = p.githubUrl || "#");
            document.querySelectorAll(".linkedin").forEach(el => el.href = p.linkedInUrl || "#");
            document.querySelectorAll(".website").forEach(el => el.href = p.websiteUrl || "#");






        }

        // --- 2️⃣ YETENEKLER ---
        const skillRes = await fetch(`${API_BASE_URL}/Skill/GetAll`);
        const skills = await skillRes.json();
        const skillsGrid = document.querySelector(".skills-grid");

        if (skillsGrid && skills?.length > 0) {
            skillsGrid.innerHTML = "";


            const grouped = skills.reduce((acc, s) => {
                if (!acc[s.category]) acc[s.category] = [];
                acc[s.category].push(s);
                return acc;
            }, {});


            Object.keys(grouped).forEach(category => {
                const categoryDiv = document.createElement("div");
                categoryDiv.classList.add("skill-category");

                categoryDiv.innerHTML = `
      <h3>${category}</h3>
      <div class="skill-items"></div>
    `;

                const itemsDiv = categoryDiv.querySelector(".skill-items");
                grouped[category].forEach(s => {
                    const item = document.createElement("div");
                    item.classList.add("skill-item");
                    item.innerHTML = `
        <div class="skill-icon"><i class="${s.iconClass || 'fas fa-code'}"></i></div>
        <span>${s.name}</span>
        <div class="skill-level" data-level="${s.level}"></div>
      `;
                    itemsDiv.appendChild(item);
                });

                skillsGrid.appendChild(categoryDiv);
            });
        }

        // --- 3️⃣ PROJELER ---
        const projectRes = await fetch(`${API_BASE_URL}/Project/GetAll`);
        const projects = await projectRes.json();
        const projectsGrid = document.querySelector(".projects-grid");

        if (projectsGrid && projects?.length > 0) {
            projects.forEach(p => {
                const el = document.createElement("div");
                el.classList.add("project-card");

                const shortDesc = p.description.length > 150 ? p.description.slice(0, 150) + "..." : p.description;

                el.innerHTML = `
    <div class="project-image">
      <img src="${p.imageUrl || 'https://via.placeholder.com/400x250'}" alt="${p.title}">
      <div class="project-overlay">
        <div class="project-links">
          ${p.githubUrl ? `<a href="${p.githubUrl}" target="_blank"><i class="fab fa-github"></i></a>` : ""}
        </div>
      </div>
    </div>
    <div class="project-content">
      <h3>${p.title}</h3>
      <p class="description" data-full="${p.description}">${shortDesc}</p>
      ${p.description.length > 150 ? `<span class="read-more" style="cursor:pointer; color:#8b5cf6; font-weight:600;">Devamını Gör</span>` : ""}
      ${p.technologies
                        ? `<div class="project-tech">${p.technologies.split(',').map(t => `<span>${t.trim()}</span>`).join('')}</div>`
                        : ""
                    }
    </div>
  `;
                projectsGrid.appendChild(el);

                const readMoreBtn = el.querySelector('.read-more');

                if (readMoreBtn) {
                    readMoreBtn.addEventListener('click', () => {
                        const desc = el.querySelector('.description');
                        desc.textContent = desc.getAttribute('data-full');
                        readMoreBtn.style.display = 'none';
                    });
                }
            });


        }








        // --- EXPERIENCE YÜKLEME ---

        const experienceRes = await fetch(`${API_BASE_URL}/Experience/GetAll`);
        const experiences = await experienceRes.json();
        const experienceGrid = document.querySelector(".experience-list");

        if (experienceGrid && experiences?.length > 0) {
            experienceGrid.innerHTML = "";

            experiences.forEach(exp => {
                const el = document.createElement("div");
                el.classList.add("exp-item");

                // Tarihleri daha okunaklı yap
                const startDate = new Date(exp.startDate);
                const endDate = exp.endDate ? new Date(exp.endDate) : null;

                const formatDate = date => `${date.getMonth() + 1}/${date.getFullYear()}`;
                const startText = startDate ? formatDate(startDate) : '';
                const endText = endDate ? formatDate(endDate) : 'Devam Ediyor';

                el.innerHTML = `
            <div class="exp-dot"></div>
            <div class="exp-content">
                <h5>${exp.position} @ ${exp.companyName}</h5>
                <span>${startText} - ${endText} | ${exp.sector} | ${exp.workingMethod}</span>
                <p>${exp.explanation}</p>
                ${exp.technologyUsed
                        ? `<div class="tech-used">${exp.technologyUsed.split(',').map(t => `<span>${t.trim()}</span>`).join('')}</div>`
                        : ''}
            </div>
        `;

                experienceGrid.appendChild(el);
            });
        }










        // --- 4️⃣ GITHUB REPO SAYISI ---

        const githubUserRes = await fetch(`https://api.github.com/users/${GITHUB_USERNAME}`);
        const githubUser = await githubUserRes.json();

        if (githubUser && githubUser.public_repos !== undefined) {
            const repoCount = githubUser.public_repos; // 👈 Doğrudan 62 geliyor
            const projectStat = document.querySelector(".stat-number[data-target='50']");
            if (projectStat) {
                projectStat.textContent = repoCount;
                projectStat.setAttribute("data-target", repoCount);
            }
        }














    } catch (err) {
        console.error("❌ API'den veri alınamadı:", err);
    }






});



