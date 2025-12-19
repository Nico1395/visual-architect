<script setup lang="ts">
import { useI18n } from "vue-i18n"
import UserMenu from "./UserMenu.vue"

const { t } = useI18n()
</script>

<template>
    <div class="header">
        <nav class="navbar">
            <a class="navbar-logo" href="/app/home">
                <img
                    src="@/assets/logo/visual-architect-logo_64x64.png"
                    alt="Visual Architect Logo"
                />

                <span> {{ t("layout.header.appname") }} </span>
            </a>

            <div class="navbar-actions">
                <UserMenu />
            </div>
        </nav>
    </div>

    <div class="content"><slot /></div>
</template>

<style scoped>
</style>

<style>
.header {
    flex: none;

    .navbar {
        background-color: var(--sidebar);
        color: var(--sidebar-foreground);
        border: 1px solid var(--border);
        border-radius: var(--radius-xl);
        height: 60px;
        padding: 1rem;
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-between;

        .navbar-logo {
            flex: none;
            display: flex;
            flex-direction: row;
            align-items: center;
            gap: 0.8rem;
            text-decoration: none;

            > img {
                height: 35px;
            }

            > span {
                font-size: 20pt;
                font-weight: 700;
            }

            &:visited {
                color: currentColor;
            }
        }

        .navbar-actions {
            flex: none;
            display: flex;
            flex-direction: row;
            align-items: center;
        }
    }
}

.content {
    flex: 1;
}
</style>
