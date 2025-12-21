<script setup lang="ts">
import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
} from '@/components/ui/card'
import { ref } from 'vue';
import { useI18n } from "vue-i18n"
import logo_64x64 from "@/assets/img/logo/visual-architect-logo_64x64.png"

const { t } = useI18n()
const splashTextKey = ref("")

function getSplashTextKey(index: number) {
    return `auth.login.splash.spl${index}`;
}

function toggleSplashText() {
    const splashTextKeyIndex = parseInt(localStorage.getItem("splash_index") ?? "0");
    splashTextKey.value = getSplashTextKey(splashTextKeyIndex);

    const nextIndex = splashTextKeyIndex >= 7 ? 0 : splashTextKeyIndex + 1;
    localStorage.setItem("splash_index", nextIndex.toString());
}

function login() {
    const returnUrl = encodeURIComponent(
        new URLSearchParams(window.location.search).get("returnUrl") || "/app/home"
    );
    window.location.href = `${import.meta.env.VITE_API_BASE_URL}/api/auth/login?p=github&r=${returnUrl}`;
}

toggleSplashText();
</script>

<template>
    <Card class="login-card">
        <CardHeader class="login-header">
            <img
                :src="logo_64x64"
                alt="Visual Architect Logo"
            />

            <span> {{ t("auth.login.appname") }} </span>
        </CardHeader>

        <CardContent class="login-content">
            <Button
                class="login-content-splash"
                variant="ghost"
                @click="toggleSplashText"
            >
                {{ t(splashTextKey) }}
            </Button>
        </CardContent>

        <CardFooter class="login-footer">
            <Button variant="github" @click="login">
                {{ t("auth.login.oauth.github") }}
            </Button>

            <Button variant="google">
                {{ t("auth.login.oauth.google") }}
            </Button>

            <Button variant="microsoft">
                {{ t("auth.login.oauth.microsoft") }}
            </Button>
        </CardFooter>

        <div class="login-copyright">
            {{ t("auth.login.copyright") }}
        </div>
    </Card>
</template>

<style>
#app:has(.login-card) {
    justify-content: center;
    align-items: center;
}

.login-card {
    width: 400px;
    position: relative;

    .login-header {
        display: flex;
        flex-direction: row;
        gap: 0.8rem;
        justify-content: center;
        align-items: center;

        > img {
            height: 35px;
        }

        > span {
            font-weight: 700;
            font-size: 22pt;
        }
    }

    .login-content {
        .login-content-splash {
            color: var(--muted-foreground);
            white-space: wrap;
            width: 100%;
            height: 5rem;
            padding: 1rem;
            cursor: pointer;
        }
    }

    .login-footer {
        display: flex;
        flex-direction: column;
        gap: 0.3rem;

        > * {
            width: 100%;
        }
    }

    .login-copyright {
        color: var(--muted-foreground);
        position: absolute;
        text-align: center;
        right: 0;
        left: 0;
        bottom: -40px;
        font-size: 10pt;
        font-weight: 600;
    }
}
</style>
