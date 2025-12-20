import { createRouter, createWebHistory } from "vue-router"
import BlankLayout from "./routes/BlankLayout.vue"
import AppLayout from "./routes/app/AppLayout.vue"
import AuthLayout from "./routes/auth/AuthLayout.vue"
import http from "./http"

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "index",
            component: () => import("./routes/IndexView.vue"),
            meta: { layout: BlankLayout },
        },
        {
            path: "/:pathMatch(.*)*",
            name: "not-found",
            component: () => import("./routes/NotFoundView.vue"),
        },
        {
            path: "/app/home",
            name: "home",
            component: () => import("./routes/app/home/HomeView.vue"),
            meta: { layout: AppLayout },
        },
        {
            path: "/auth/login",
            name: "login",
            component: () => import("./routes/auth/login/LoginView.vue"),
            meta: { layout: AuthLayout },
        },
        {
            path: "/auth/register",
            name: "register",
            component: () => import("./routes/auth/register/RegisterView.vue"),
            meta: { layout: AuthLayout },
        },
    ],
})

const publicNames = ["index", "login"];
router.beforeEach(async (to, from, next) => {
    const nextName = to.name?.toString();
    if (nextName && publicNames.includes(nextName)) {
        return next();
    }

    try {
        await http.get("/api/auth/me");
        next();
    } catch {
        next({ path: "/auth/login", query: { returnUrl: to.fullPath } });
    }
});

export default router
