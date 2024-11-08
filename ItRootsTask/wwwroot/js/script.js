const translations = {
    en: {
        loginTitle: "Login",
        usernamePlaceholder: "Username",
        passwordPlaceholder: "Password",
        captchaPlaceholder: "Enter code",
        signIn: "Sign In",
        register: "Register",
        captchaError: "Incorrect captcha. Please try again."
    },
    ar: {
        loginTitle: "تسجيل الدخول",
        usernamePlaceholder: "اسم المستخدم",
        passwordPlaceholder: "كلمة المرور",
        captchaPlaceholder: "أدخل الرمز",
        signIn: "تسجيل الدخول",
        register: "إنشاء حساب",
        captchaError: "الكود المدخل غير صحيح. يرجى المحاولة مرة أخرى."
    }
};

function updateTextContent(lang) {
    const { loginTitle, usernamePlaceholder, passwordPlaceholder, captchaPlaceholder, signIn, register } = translations[lang];

    document.getElementById('login-title').textContent = loginTitle;
    document.getElementById('username-placeholder').placeholder = usernamePlaceholder;
    document.getElementById('password-placeholder').placeholder = passwordPlaceholder;
    document.getElementById('captcha-input').placeholder = captchaPlaceholder;
    document.getElementById('sign-in-btn').textContent = signIn;
    document.getElementById('register-btn').textContent = register;
}

const languageIcon = document.getElementById('language-icon');
languageIcon.addEventListener('click', () => {
    const html = document.querySelector('html');
    const lang = html.getAttribute('lang') === 'en' ? 'ar' : 'en';

    html.setAttribute('lang', lang);
    html.style.direction = lang === 'ar' ? 'rtl' : 'ltr';
    updateTextContent(lang);
    document.getElementById('loginContainer').classList.toggle('rtl', lang === 'ar');
});

function generateCaptcha() {
    const captcha = Math.floor(100000 + Math.random() * 900000).toString();
    document.getElementById('captcha-display').value = captcha;
}

function validateCaptcha() {
    const generatedCaptcha = document.getElementById('captcha-display').value;
    const userCaptcha = document.getElementById('captcha-input').value;
    const errorMessage = translations[document.querySelector('html').getAttribute('lang')].captchaError;
    const captchaErrorSpan = document.getElementById('captchaError');

    if (generatedCaptcha === userCaptcha) {
        captchaErrorSpan.textContent = ""; 
        return true;
    } else {
        captchaErrorSpan.textContent = errorMessage;
        generateCaptcha(); 
        return false;
    }
}

window.onload = generateCaptcha;
