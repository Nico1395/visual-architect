import { createRouter, createWebHistory } from "vue-router"
import AppLayout from "./routes/app/AppLayout.vue"
import AuthLayout from "./routes/auth/AuthLayout.vue"
import { isAuthenticated } from "./auth"
import { useInitializationStore } from "./persistence/stores/initialization.store"
import i18n from "./i18n"

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [{
        path: "/auth",
        redirect: "/auth/login",
        component: AuthLayout,
        children: [{
                path: "login",
                name: "login",
                component: () => import("./routes/auth/login/LoginView.vue"),
            },
        ],
    },
    {
        path: "/app",
        redirect: "/app/home",
        component: AppLayout,
        children: [
        {
            path: "home",
            name: "home",
            component: () => import("./routes/app/home/HomeView.vue"),
        },
        {
            path: "settings",
            redirect: "/app/settings/profile",
            name: "settings",
            component: () => import("./routes/app/settings/SettingsLayout.vue"),
            children: [
                {
                    path: "profile",
                    name: "profilesettings",
                    component: () => import("./routes/app/settings/profile/ProfileSettingsView.vue"),
                },
                {
                    path: "personalization",
                    name: "personalizationsettings",
                    component: () => import("./routes/app/settings/personalization/PersonalizationSettingsView.vue"),
                },
                {
                    path: "account",
                    name: "accountsettings",
                    component: () => import("./routes/app/settings/account/AccountSettingsView.vue"),
                },
            ]
        }
        ],
        },
        {
            path: "/:pathMatch(.*)*",
            name: "notfound",
            component: () => import("./routes/NotFoundView.vue"),
        },
    ],
})

const publicRouteNames = ["login"];

router.beforeEach(async (to, _, next) => {
    const initializationStore = useInitializationStore()
    if (!initializationStore.initialized)
        await initializationStore.initialize()

    const nextName = to.name?.toString();
    if (nextName && publicRouteNames.includes(nextName)) {
        return next();
    }

    if (await isAuthenticated()) {
        if (to.name === "notfound") {
            return next({ name: "home" });
        }

        return next();
    }

    next({ path: "/auth/login", query: { returnUrl: to.fullPath } });
});

router.afterEach((to) => {
  document.title = to.name ? i18n.global.t(`pages.${to.name.toString()}`) : i18n.global.t('pages.app');
});

export default router
