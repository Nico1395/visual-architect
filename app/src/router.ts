import { createRouter, createWebHistory } from "vue-router"
import BlankLayout from "./routes/BlankLayout.vue"
import AppLayout from "./routes/app/AppLayout.vue"

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
            path: "/app/home",
            name: "home",
            component: () => import("./routes/app/home/HomeView.vue"),
            meta: { layout: AppLayout },
        },
    ],
})

export default router
