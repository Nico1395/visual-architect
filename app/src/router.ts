import { createRouter, createWebHistory } from "vue-router"
import AppLayout from "./routes/app/AppLayout.vue"
import AuthLayout from "./routes/auth/AuthLayout.vue"
import { isAuthenticated } from "./auth"

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
        {
            path: "register",
            name: "register",
            component: () => import("./routes/auth/register/RegisterView.vue"),
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
                    name: "profile-settings",
                    component: () => import("./routes/app/settings/profile/ProfileSettingsView.vue"),
                },
                {
                    path: "personalization",
                    name: "personalization-settings",
                    component: () => import("./routes/app/settings/personalization/PersonalizationSettingsView.vue"),
                },
                {
                    path: "account",
                    name: "account-settings",
                    component: () => import("./routes/app/settings/account/AccountSettingsView.vue"),
                },
            ]
        }
        ],
        },
        {
            path: "/:pathMatch(.*)*",
            name: "not-found",
            component: () => import("./routes/NotFoundView.vue"),
        },
    ],
})

const publicNames = ["login"];
router.beforeEach(async (to, _, next) => {
    const nextName = to.name?.toString();
    if (nextName && publicNames.includes(nextName)) {
        return next();
    }

    if (await isAuthenticated()) {
        return next();
    }

    next({ path: "/auth/login", query: { returnUrl: to.fullPath } });
});

export default router
