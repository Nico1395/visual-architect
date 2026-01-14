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
            path: "preferences",
            redirect: "/app/preferences/profile",
            name: "preferences",
            component: () => import("./routes/app/preferences/PreferencesLayout.vue"),
            children: [
                {
                    path: "profile",
                    name: "profilepreferences",
                    component: () => import("./routes/app/preferences/profile/ProfilePreferencesView.vue"),
                },
                {
                    path: "personalization",
                    name: "personalizationpreferences",
                    component: () => import("./routes/app/preferences/personalization/PersonalizationPreferencesView.vue"),
                },
                {
                    path: "account",
                    name: "accountpreferences",
                    component: () => import("./routes/app/preferences/account/AccountPreferencesView.vue"),
                },
            ]
        },
        {
            path: "design-projects/:projectId",
            redirect: to => ({
                name: "design-projects-overview",
                params: { projectId: to.params.projectId },
            }),
            name: "design-projects",
            component: () => import("./routes/app/design-projects/DesignProjectLayout.vue"),
            children: [
                {
                    path: "overview",
                    name: "design-projects-overview",
                    component: () => import("./routes/app/design-projects/overview/DesignProjectOverviewView.vue"),
                },
                {
                    path: "tasks",
                    name: "design-projects-tasks",
                    component: () => import("./routes/app/design-projects/tasks/DesignProjectTasksView.vue"),
                },
                {
                    path: "settings",
                    name: "design-projects-settings",
                    component: () => import("./routes/app/design-projects/settings/DesignProjectSettingsView.vue"),
                },
            ]
        }],
    },
    {
        path: "/:pathMatch(.*)*",
        name: "notfound",
        component: () => import("./routes/NotFoundView.vue"),
    }],
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
