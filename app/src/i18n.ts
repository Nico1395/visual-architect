import { createI18n } from "vue-i18n"

const en = {
    layout: {
        header: {
            appname: "Visual Architect",
            usermenu: {
                settings: "Settings",
                settingsdesc: "Account management and settings",
                logout: "Log Out",
                logoutdesc: "Sad to see you leave already! 🙁",
                signedinas: "Signed in as",
            },
        },
    },
    auth: {
        login: {
            appname: "Visual Architect",
            copyright: "© 2026 Nicolai Wolf",
            prompt: "Login",
            oauth:  {
                github: "Login with GitHub",
                google: "Login with Google",
                microsoft: "Login with Microsoft"
            },
            splash: {
                "spl0": "Your designs start here! Ready to make some magic? ✨",
                "spl1": "Time to visualize! Systems are fun when they look this good. 😎",
                "spl2": "Systems running on coffee? Log in and get your projects moving! ☕",
                "spl3": "Got a new feature? Visualize your specs before the standup. 💬",
                "spl4": "Management, ... eh? 🫠",
                "spl5": "Sales sold that new feature btw. Oh you know, the one you don't even know about yet. 💵",
                "spl6": "Customer ordered yesterday, sales wanted it done the day before that. 😐",
                "spl7": "Let's design something the devs will garble. 😒",
            }
        },
        register: {
        }
    }
}

const de = {
    layout: {
        header: {
            appname: "Visual Architect",
            usermenu: {
                settings: "Einstellungen",
                settingsdesc: "Kontoverwaltung und Einstellungen",
                logout: "Abmelden",
                logoutdesc: "Schade, dass du schon gehst! 🙁",
            },
        },
    },
    auth: {
        login: {
            appname: "Visual Architect",
            copyright: "© 2026 Nicolai Wolf",
            prompt: "Anmelden",
            oauth:  {
                github: "Anmelden mit GitHub",
                google: "Anmelden mit Google",
                microsoft: "Anmelden mit Microsoft"
            },
            splash: {
                "spl0": "Deine Designs starten hier! Bereit für ein bisschen Magie? ✨",
                "spl1": "Zeit zu visualisieren! Systeme sind schöner, wenn sie so gut aussehen. 😎",
                "spl2": "Eure Systeme laufen mal wieder nur mit Kaffee? Meld' an und bring deine Projekte in Schwung! ☕",
                "spl3": "Neue Features am Start? Visualisiere deine Specs vor dem Stand-up. 💬",
                "spl4": "Management, ... ne? 🫠",
                "spl5": "Der Vertrieb hat das neue Feature verkauft. Du weißt schon, das, von dem du noch gar nichts weißt. 💵",
                "spl6": "Kunde hats gestern bestellt. Vertrieb wollte's vorgestern fertig haben. 😐",
                "spl7": "Lass uns etwas designen, damit's die Entwickler versauen. 😒",
            }
        },
        register: {
        }
    }
}

const i18n = createI18n({
    locale: import.meta.env.VITE_DEFAULT_LOCALE ?? "en",
    fallbackLocale: import.meta.env.VITE_FALLBACK_LOCALE ?? "en",
    legacy: false,
    messages: {
        en,
        de,
    },
})

export default i18n
