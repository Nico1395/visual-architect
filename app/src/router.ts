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
                    name: "profilePreferences",
                    component: () => import("./routes/app/preferences/profile/ProfilePreferencesView.vue"),
                },
                {
                    path: "personalization",
                    name: "personalizationPreferences",
                    component: () => import("./routes/app/preferences/personalization/PersonalizationPreferencesView.vue"),
                },
                {
                    path: "account",
                    name: "accountPreferences",
                    component: () => import("./routes/app/preferences/account/AccountPreferencesView.vue"),
                },
            ]
        },
        {
            path: "design-projects/:projectId",
            redirect: to => ({
                name: "designProjectOverview",
                params: { projectId: to.params.projectId },
            }),
            name: "designProject",
            component: () => import("./routes/app/design-projects/DesignProjectLayout.vue"),
            children: [
                {
                    path: "overview",
                    name: "designProjectOverview",
                    component: () => import("./routes/app/design-projects/overview/DesignProjectOverviewView.vue"),
                },
                {
                    path: "tasks",
                    name: "designProjectTasks",
                    component: () => import("./routes/app/design-projects/tasks/DesignProjectTasksView.vue"),
                },
                {
                    path: "settings",
                    name: "designProjectSettings",
                    component: () => import("./routes/app/design-projects/settings/DesignProjectSettingsView.vue"),
                },
            ]
        },
        {
            path: "design-projects/:projectId/tasks/:taskNumber",
            name: "designTask",
            component: () =>
                import("./routes/app/design-projects/tasks/DesignTaskView.vue"),
        },],
    },
    {
        path: "/:pathMatch(.*)*",
        name: "notFound",
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
        if (to.name === "notFound") {
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
