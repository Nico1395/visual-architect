import "./style.css"
import 'vue-sonner/style.css';

import { createApp } from "vue"

import App from "./App.vue"
import router from "./router"
import i18n from "./i18n"
import pinia from "./pinia"

const app = createApp(App)

app.use(pinia)
app.use(i18n)
app.use(router)

app.mount("#app")
