(function () {
    let token = "";

    // افزودن فرم لاگین
    const authButton = document.createElement("button");
    authButton.innerText = "Login";
    authButton.style = "margin-left: 10px;";
    authButton.onclick = async function () {
        const username = prompt("Enter username:");
        const password = prompt("Enter password:");
        if (!username || !password) {
            alert("Username and password are required!");
            return;
        }

        // فراخوانی API لاگین
        const BASE_URL = "http://localhost:5169";
        const response = await fetch(`${BASE_URL}/api/Account/Login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        });

        if (response.ok) {
            const data = await response.json();
            token = data.token;

            // تنظیم توکن در Swagger
            const bearerAuth = `Bearer ${token}`;
            document.querySelectorAll('[data-name="authorization"]')
                .forEach((input) => {
                    input.value = bearerAuth;
                    input.dispatchEvent(new Event('input'));
                });

            alert("Logged in successfully!");
        } else {
            alert("Login failed. Please check your credentials.");
        }
    };

    // افزودن دکمه به منوی بالای Swagger
    const topbar = document.querySelector(".swagger-ui .topbar");
    if (topbar) {
        topbar.appendChild(authButton);
    }
})();
