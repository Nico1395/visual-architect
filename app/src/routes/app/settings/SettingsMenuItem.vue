<script setup lang="ts">
import { cn } from "@/lib/utils"
import { watch , ref } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();
const props = defineProps<{
    to: string,
    class?: string | null
}>();

const active = ref(route.path === props.to)
watch(() => route.path, (newPath) => active.value = newPath === props.to);

function getActiveClass() {
    return active.value ? "active" : "";
}
</script>

<template>
    <RouterLink
        :class="cn(
            'data-active:focus:bg-accent data-active:hover:bg-accent data-active:bg-accent/50 data-active:text-accent-foreground hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground ring-ring/10 dark:ring-ring/20 dark:outline-ring/40 outline-ring/50 [&_svg:not([class*=\'text-\'])]:text-muted-foreground flex flex-col gap-1 rounded-sm p-2 text-sm transition-[color,box-shadow] focus-visible:ring-4 focus-visible:outline-1 [&_svg:not([class*=\'size-\'])]:size-4 settings-menu-item',
            props.class,
            getActiveClass())"
        :to="props.to"
    >
        <slot />
    </RouterLink>
</template>

<style>
    .settings-menu-item {
        display: flex;
        flex-direction: row;
        flex-wrap: nowrap;
        align-items: center;
        gap: 0.5rem;
        font-size: 11pt;
        position: relative;

        &.active {
            font-weight: 700;
        }

        > .icon {
            scale: 1.1;
        }

    }
</style>
