<script setup lang="ts">
import { useI18n } from "vue-i18n"
import UserMenu from "./UserMenu.vue"
import logo_64x64 from "@/assets/img/logo/visual-architect-logo_64x64.png"
import FooterItem from "./FooterItem.vue";
import Icon from "@/components/Icon.vue";
import ExternalFooterItem from "./ExternalFooterItem.vue";

const { t } = useI18n()
</script>

<template>
    <div class="header">
        <nav class="navbar">
            <router-link class="navbar-logo" to="/app/home">
                <img
                    :src="logo_64x64"
                    alt="Visual Architect Logo"
                />

                <span> {{ t("layout.header.appname") }} </span>
            </router-link>

            <div class="navbar-actions">
                <UserMenu />
            </div>
        </nav>
    </div>

    <div class="content">
        <router-view />
    </div>

    <div class="footer">
        <ExternalFooterItem to="https://github.com/Nico1395/visual-architect">
            <Icon icon="ai-github-fill" />

            Open Source
        </ExternalFooterItem>

        <FooterItem to="/app/about">
            About
        </FooterItem>
    </div>
</template>

<style>
.header {
    flex: none;

    .navbar {
        background-color: var(--sidebar);
        color: var(--sidebar-foreground);
        border-bottom: 1px solid var(--border);
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
    margin: 2.5rem 0 10rem 0;
}

.footer {
    padding: 3rem 10rem;
    color: var(--muted-foreground);
    font-size: 9pt;
    flex: none;
    display: flex;
    flex-direction: row;
    justify-content: center;
    flex-wrap: wrap;
    gap: 1.5rem;
}
</style>
