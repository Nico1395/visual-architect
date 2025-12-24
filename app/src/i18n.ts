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
    pages: {
        app: "Visual Architect",
        login: "Login",
        home: "Home",
        settings: "Preferences",
        profilesettings: "Profile Preferences",
        personalizationsettings: "Personalization Preferences",
        accountsettings: "Account Preferences",
        notfound: "Not Found",
    },
    toasts: {
        saving: {
            loading: "Saving...",
            success: "Saved successfully!",
            error: "Encountered an error saving. 🫥",
        }
    },
    notfound: {
        message: "Oops, looks like this page does not exist! 🥺",
        home: "Better to go back to the homepage...",
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
    },
    settings: {
        header: {
            save: "Save",
            reset: "Reset",
        },
        profile: {
            menuitem: "Profile",
            header: "Profile preferences",
            description: "Update and manage your personal information. Change your display name, email address, or avatar, and control who can see your profile. Keep your presence in the app exactly how you want it.",
            email: "E-mail",
            emaildesc: "Your e-mail address is only used for contacting you. Changing it does not have an effect on how you authenticate yourself in the app.",
            displayname: "Display name",
        },
        account: {
            menuitem: "Account",
            header: "Account preferences",
            description: "Access key account preferences and management options. Here you can review your account details, adjust security settings, and delete your account if needed. Everything related to your account’s status and control is in one place.",
            delete: {
                title: "Delete Account",
                description: "Deleting your account removes your identity and all data associated with it from our databases. This action is non-reversable.",
                button: "Delete",
                modal: {
                    title: "Deleting your account",
                    description: "You are about to delete your account. Do you really want to do that?",
                    cancel: "No I dont",
                    confirm: "Yes I do",
                },
                toast: {
                    loading: "Deleting your account...",
                    success: "Account deleted successfully 🙁",
                    error: "Error deleting your account",
                }
            },
        },
        personalization: {
            menuitem: "Personalization",
            header: "Personalization preferences",
            description: "Tailor the app to fit your style and preferences. Adjust the theme, switch between light and dark modes, change colors, and set your preferred language to create the experience that works best for you.",
            theme: {
                title: "Theme",
                usesystem: "Use system theme",
                light: "Light",
                dark: "Dark",
                description: "Your selected theme will be persisted across devices.",
            },
            language: {
                title: "Language",
                prompt: "Select a language",
                search: "Search language...",
                notfound: "Not available 🙁",
            },
        },
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
                signedinas: "Angemeldet als",
            },
        },
    },
    pages: {
        app: "Visual Architect",
        login: "Einloggen",
        home: "Startseite",
        settings: "Einstellungen",
        profilesettings: "Profileinstellungen",
        personalizationsettings: "Personalisierung",
        accountsettings: "Kontoeinstellungen",
        notfound: "Nicht gefunden",
    },
    toasts: {
        saving: {
            loading: "Speichere...",
            success: "Erfolgreich gespeichert!",
            error: "Fehler beim Speichern. 🫥",
        }
    },
    notfound: {
        message: "Oops, sieht so aus als würde die Seite nicht existieren! 🥺",
        home: "Geh' besser wieder zur Startseite zurück...",
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
    },
    settings: {
        header: {
            save: "Speichern",
            reset: "Zurücksetzen",
        },
        profile: {
            menuitem: "Profil",
            header: "Profileinstellungen",
            description: "Aktualisieren und verwalten Sie Ihre persönlichen Informationen. Ändern Sie Ihren Anzeigenamen, Ihre E-Mail-Adresse oder Ihr Avatar und steuern Sie, wer Ihr Profil sehen kann. Halten Sie Ihre Präsenz in der App genau so, wie Sie es möchten.",
            email: "E-Mail",
            emaildesc: "Deine E-Mail-Adresse wird ausschließlich verwendet um dich zu erreichen. Sie zu ändern sorgt nicht dafür, dass du dich in der App anders authentifizieren müsstest.",
            displayname: "Anzeigename",
        },
        account: {
            menuitem: "Konto",
            header: "Kontoeinstellungen",
            description: "Greifen Sie auf wichtige Kontoeinstellungen und Verwaltungsoptionen zu. Hier können Sie Ihre Kontodaten überprüfen, Sicherheitseinstellungen anpassen und Ihr Konto bei Bedarf löschen. Alles, was den Status und die Kontrolle Ihres Kontos betrifft, finden Sie an einem Ort.",
            delete: {
                title: "Konto löschen",
                description: "Das Löschen deines Kontos löscht deine Identität und alle mit ihr in Verbindung stehenden Daten. Diese Aktion kann nicht rückgängig gemacht werden.",
                button: "Löschen",
                modal: {
                    title: "Deinen Account löschen",
                    description: "Du löscht gerade dein Konto. Möchtest du das wirklich tun?",
                    cancel: "Nein, möchte ich nicht",
                    confirm: "Ja, möchte ich",
                },
                toast: {
                    loading: "Lösche deinen Account...",
                    success: "Account erfolgreich gelöscht 🙁",
                    error: "Fehler beim Löschen deines Accounts",
                }
            },
        },
        personalization: {
            menuitem: "Personalisierung",
            header: "Personalisierung",
            description: "Passen Sie die App an Ihren Stil und Ihre Vorlieben an. Ändern Sie das Theme, wechseln Sie zwischen Hell- und Dunkelmodus, passen Sie Farben an und wählen Sie Ihre bevorzugte Sprache, um das Erlebnis zu schaffen, das am besten zu Ihnen passt.",
            theme: {
                title: "Farbschema",
                usesystem: "Systemfarbschema verwenden",
                light: "Hell",
                dark: "Dunkel",
                description: "Dein gewähltes Farbschema wird über deine Geräte hinaus gespeichert.",
            },
            language: {
                title: "Language",
                prompt: "Sprache auswählen",
                search: "Sprache suchen...",
                notfound: "Nicht verfügbar 🙁",
            },
        },
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
