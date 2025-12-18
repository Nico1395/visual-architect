import { createI18n } from "vue-i18n"

const en = {
    layout: {
        header: {
            appname: "Visual Architect",
        },
    },
}

const de = {
    layout: {
        header: {
            appname: "Visual Architect",
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
