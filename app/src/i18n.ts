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
            },
        },
    },
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
