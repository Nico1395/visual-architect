<script setup lang="ts">
import { useI18n } from "vue-i18n"

const { t } = useI18n()
</script>

<template>
    <div class="header">
        <nav class="navbar">
            <a class="navbar-logo" href="/app/home">
                <img />

                <span> {{ t("layout.header.appname") }} </span>
            </a>

            <div class="navbar-actions"></div>
        </nav>
    </div>

    <div class="content"><slot /></div>
</template>

<style>
html,
body,
#app {
    margin: 0;
    height: 100%;
}

body {
    font-family: "Inter", sans-serif;
}

#app {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 1rem;
    box-sizing: border-box;
}

.header {
    flex: none;

    .navbar {
        border: 1px solid black;
        height: 60px;

        .navbar-logo {
            > img {
            }
        }

        .navbar-actions {
        }
    }
}

.content {
    flex: 1;
}
</style>
