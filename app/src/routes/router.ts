import { createRouter, createWebHistory } from "vue-router"
import BlankLayout from "./BlankLayout.vue"
import AppLayout from "./app/AppLayout.vue"

export const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "index",
            component: () => import("./IndexView.vue"),
            meta: { layout: BlankLayout },
        },
        {
            path: "/app/home",
            name: "home",
            component: () => import("./app/home/HomeView.vue"),
            meta: { layout: AppLayout },
        },
    ],
})
