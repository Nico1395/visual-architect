import { createI18n } from "vue-i18n"

const en = {
    components: {
        mdeditor: {
            write: "Write",
            preview: "Preview",
            placeholder: "Write some Markdown...",
        },
        markdownArea: {
            placeholder: "No content yet.",
            undo: "Undo",
            redo: "Redo",
            header1: "Heading 1",
            header2: "Heading 2",
            header3: "Heading 3",
            bold: "Bold",
            italic: "Italic",
            strikethrough: "Strikethrough",
        }
    },
    layout: {
        header: {
            appname: "Visual Architect",
            usermenu: {
                preferences: "Preferences",
                preferencesdesc: "Account management and preferences",
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
        preferences: "Preferences",
        profilepreferences: "Profile Preferences",
        personalizationpreferences: "Personalization Preferences",
        accountpreferences: "Account Preferences",
        notfound: "Not Found",
    },
    toasts: {
        saving: {
            loading: "Saving...",
            success: "Saved successfully!",
            error: "Encountered an error saving. 🫥",
        },
        deleting: {
            loading: "Deleting...",
            success: "Deleted successfully!",
            error: "Encountered an error deleting. 🫥",
        }
    },
    actions: {
        okay: "Okay",
        accept: "Accept",
        confirm: "Confirm",
        new: "New",
        cancel: "Cancel",
        save: "Save",
        create: "Create",
        discard: "discard",
        edit: "Edit",
    },
    shortcuts: {
        undo: "{modifier}+Z",
        redo: "{modifier}+Y",
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
    preferences: {
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
    },
    home: {
        proj: {
            title: "Your design projects",
            new: "New",
            loading: "Loading your design projects...",
            noproj: "You don't have any design projects yet. Hit 'New' to create your first design project!",
        },
        newprojdg: {
            title: "New design project",
            description: "Create a new home for designs youre organizing for a project or an entire application. We recommend to keep your conventions of how youre organizing your tasks and designs consinstent across design projects.",
            namelabel: "Name of the design project",
            descriptionlabel: "Describe the design project",
            cancel: "Cancel",
            create: "Create",
        }
    },
    designprojects: {
        overview: {
            title: "Overview",
            description: {
                title: "Description",
                none: "There is no description yet. Why don't you provide one?",
                editTitle: "Editing",
            },
            taskStats: {
                title: "Task Statistics"
            },
        },
        tasks: {
            title: "Tasks",
            new: "New",
            filters: {
                search: "Search for tasks..."
            },
            list: {
                notasks: "No tasks yet, create some by hitting 'New'!",
                task: {
                    status: {
                        todo: "Todo",
                        progress: "In progress",
                        completed: "Completed",
                    },
                    createdat: "Created at",
                    updatedat: "Updated at",
                },
            },
            formDialog: {
                title: "New design task",
                description: "Eine Designaufgabe stellt eine Arbeitseinheit innerhalb eines Designprojekts dar. In einer Designaufgabe kannst du mehrere Designs anlegen um mehrere Lösungsansätze für das zu lösende Problem entwerfen zu können.",
                nameLabel: "Name of the design task",
                descriptionLabel: "Description of the design task",
                discard: "Discard",
                create: "Create",
            }
        },
        settings: {
            title: "Settings",
            name: {
                label: "Name",
                description: "Changing the name of the design project does not affect any links, references or other features involving design projects. Project names are not unique on the platform or your account."
            },
            delete: {
                title: "Delete project",
                description: "Deleting a project also deletes all data associated with it, like its tasks and their designs. This action is not reversable.",
                callToAction: "Delete",
                modal: {
                    title: "Deleting the project",
                    description: "You are about to the project and all tasks and designs associated with it. Do you really want to do that?",
                    cancel: "No I dont",
                    confirm: "Yes I do",
                },
                toast: {
                    loading: "Deleting your the design project...",
                    success: "Design project deleted successfully",
                    error: "Error deleting the design project 🫥",
                }
            },
        },
    },
    designTask: {
        header: {
            name: {
                placeholder: "Enter a name for the design task"
            }
        },
        status: {
            name: "Status:",
            todo: "Todo",
            progress: "In progress",
            completed: "Completed",
            setStatus: "Set status"
        },
        menu: {
            deleteTask: {
                item: "Delete design task",
                modal: {
                    title: "Deleting the task",
                    description: "You are about to delete the design task and all designs associated with it. Do you really want to do that?",
                },
            }
        },
        description: {
            title: "Description",
        },
        designs: {
            title: "Designs",
            noDesigns: "No designs yet. Hit 'New' and create sum more!",
        }
    },
}

const de = {
    components: {
        markdownEditor: {
            write: "Bearbeiten",
            preview: "Vorschau",
            placeholder: "Schreib' etwas Markdown...",
        },
        markdownArea: {
            placeholder: "Bisher gibt es noch keine Inhalte.",
            undo: "Rückgängig machen",
            redo: "Wiederholen",
            header1: "Überschrift 1",
            header2: "Überschrift 2",
            header3: "Überschrift 3",
            bold: "Fett",
            italic: "Kursiv",
            strikethrough: "Durchgestrichen",
        }
    },
    layout: {
        header: {
            appname: "Visual Architect",
            usermenu: {
                preferences: "Einstellungen",
                preferencesdesc: "Kontoverwaltung und Einstellungen",
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
        preferences: "Einstellungen",
        profilepreferences: "Profileinstellungen",
        personalizationpreferences: "Personalisierung",
        accountpreferences: "Kontoeinstellungen",
        notfound: "Nicht gefunden",
    },
    toasts: {
        saving: {
            loading: "Speichere...",
            success: "Erfolgreich gespeichert!",
            error: "Fehler beim Speichern 🫥",
        },
        deleting: {
            loading: "Lösche...",
            success: "Erfolgreich gelöscht!",
            error: "Fehler beim Löschen 🫥",
        }
    },
    actions: {
        okay: "In Ordnung",
        accept: "Akzeptieren",
        confirm: "Bestätigen",
        new: "Neu",
        cancel: "Abbrechen",
        save: "Speichern",
        create: "Erstellen",
        discard: "Verwerfen",
        edit: "Bearbeiten",
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
    preferences: {
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
                title: "Sprache",
                prompt: "Sprache auswählen",
                search: "Sprache suchen...",
                notfound: "Nicht verfügbar 🙁",
            },
        },
    },
    home: {
        proj: {
            title: "Deine Designprojekte",
            new: "Neu",
            loading: "Lade deine Designprojekte...",
            noproj: "Du hast noch keine Designprojekte. Klick auf 'Neu' um dein erstes Designprojekt zu erstellen!",
        },
        newprojdg: {
            title: "Neues Designprojekt",
            description: "Erstelle ein neues Zuhause für Designs von einem deiner Projekte, oder einer ganzen Anwendung. Wir empfehlen, sich an eine einzige, über Designprojekte-konsistente Konvention für die Organisation von Aufgaben und Designs zu halten.",
            namelabel: "Name des Designprojekts",
            descriptionlabel: "Beschreibe das Designprojekt",
            cancel: "Abbrechen",
            create: "Erstellen",
        }
    },
    designprojects: {
        overview: {
            title: "Übersicht",
            description: {
                title: "Beschreibung",
                none: "Bisher gibt es noch keine Beschreibung. Warum schreibst du nicht eine?",
                editTitle: "Bearbeiten",
            },
            taskStats: {
                title: "Statistik Designaufgaben"
            },
        },
        tasks: {
            title: "Designaufgaben",
            new: "Neu",
            filters: {
                search: "Suche nach Designaufgaben..."
            },
            list: {
                notasks: "Noch keine Designaufgaben. Klick auf 'Neu' und erstell doch mal ein paar neue!",
                task: {
                    status: {
                        todo: "Neu",
                        progress: "In Bearbeitung",
                        completed: "Abgeschlossen",
                    },
                    createdat: "Erstellt am",
                    updatedat: "Geändert am",
                },
            },
            formDialog: {
                title: "Neue Designaufgabe",
                description: "Eine Designaufgabe stellt eine Arbeitseinheit innerhalb eines Designprojekts dar. In einer Designaufgabe kannst du mehrere Designs anlegen um mehrere Lösungsansätze für das zu lösende Problem entwerfen zu können.",
                nameLabel: "Name der Designaufgabe",
                descriptionLabel: "Name der Designaufgabe",
                discard: "Verwerfen",
                create: "Erstellen",
            }
        },
        settings: {
            title: "Einstellungen",
            name: {
                label: "Name",
                description: "Das Ändern des Namens hat keinen Einfluss auf Links, Querverweise oder Funktionen die das Designprojekt beinhalten. Namen von Designprojekten sind nicht auf der Platform or deinem Profil einzigartig."
            },
            delete: {
                title: "Designprojekt löschen",
                description: "Das Löschen eines Designprojektes, löscht auch zugehörige Daten, wie Designaufgaben und dessen Designs. Diese Aktion kann nicht rückgängig gemacht werden.",
                callToAction: "Löschen",
                modal: {
                    title: "Das Designprojekt löschen",
                    description: "Du löscht gerade das Designprojekt und alle seine zugehörigen Daten, wie Designaufgaben und dessen Designs. Möchtest du das wirklich tun?",
                    cancel: "Nein, möchte ich nicht",
                    confirm: "Ja, möchte ich",
                },
                toast: {
                    loading: "Lösche das Designprojekt...",
                    success: "Designprojekt erfolgreich gelöscht",
                    error: "Fehler beim Löschen des Designprojekts 🫥",
                }
            },
        },
    },
    designTask: {
        header: {
            name: {
                placeholder: "Gib einen Namen für die Designaufgabe ein"
            }
        },
        status: {
            name: "Zustand:",
            todo: "Neu",
            progress: "In Bearbeitung",
            completed: "Abgeschlossen",
            setStatus: "Zustand aktualisieren"
        },
        menu: {
            deleteTask: {
                item: "Designaufgabe löschen",
                modal: {
                    title: "Die Designaufgabe löschen",
                    description: "Du löscht gerade die Designaufgabe und alle zugehörigen Daten, wie Designs. Möchtest du das wirklich tun?",
                },
            }
        },
        description: {
            title: "Beschreibung",
        },
        designs: {
            title: "Designs",
            noDesigns: "Noch keine Designs. Klick 'Neu' und erstell' 'n paar!",
        }
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
