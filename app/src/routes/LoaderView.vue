<script setup lang="ts">
import {
  Card,
  CardContent,
  CardHeader,
} from '@/components/ui/card'
import logo_64x64 from "@/assets/img/logo/visual-architect-logo_64x64.png"
import Spinner from '@/components/ui/spinner/Spinner.vue';
</script>

<template>
    <Card class="loader-card">
        <CardHeader class="loader-header">
            <img
                :src="logo_64x64"
                alt="Visual Architect Logo"
            />

            <span>
                Visual Architect
            </span>
        </CardHeader>

        <CardContent class="loader-content">
            <Spinner class="spinner" />
        </CardContent>

        <div class="loader-copyright">
            © 2026 Nicolai Wolf
        </div>
    </Card>
</template>

<style>
#app:has(.loader-card) {
    justify-content: center;
    align-items: center;
}

.loader-card {
    width: 400px;
    position: relative;

    .loader-header {
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

    .loader-content {
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 2rem 0;

        > .spinner {
            height: 30px;
            width: 30px;
        }
    }

    .loader-copyright {
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
