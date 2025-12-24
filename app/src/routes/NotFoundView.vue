<script setup lang="ts">
import {
  Card,
  CardContent,
  CardHeader,
} from '@/components/ui/card'
import logo_64x64 from "@/assets/img/logo/visual-architect-logo_64x64.png"
import { useI18n } from 'vue-i18n';
import CardFooter from '@/components/ui/card/CardFooter.vue';

const { t } = useI18n()
</script>

<template>
    <Card class="not-found-card">
        <CardHeader class="not-found-header">
            <img
                :src="logo_64x64"
                alt="Visual Architect Logo"
            />

            <span>
                Visual Architect
            </span>
        </CardHeader>

        <CardContent class="not-found-content">
            <h1>404</h1>

            <p>
                {{ t("notfound.message") }}
            </p>
        </CardContent>

        <CardFooter class="not-found-footer">
            <RouterLink class="homepage-link" to="/app/home">
                {{ t("notfound.home") }}
            </RouterLink>
        </CardFooter>
    </Card>
</template>

<style>
#app:has(.not-found-card) {
    justify-content: center;
    align-items: center;
}

.not-found-card {
    width: 400px;
    position: relative;

    .not-found-header {
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

    .not-found-content {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        padding: 1rem 2rem;
        gap: 1rem;

        > h1 {
            font-weight: 700;
            font-size: 30pt;
            color: var(--primary);
        }

        > p {
            text-align: center;
            font-size: 11pt;
            font-weight: 500;
        }
    }

    .not-found-footer {
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 1rem 2rem;

        .homepage-link {
            font-size: 10pt;
            font-weight: 500;
            color: var(--muted-foreground);
            transition: all 0.03s ease-in-out;

            &:hover {
                text-decoration: 1px solid underline;
                color: var(--foreground);
            }
        }
    }

    .not-found-copyright {
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
