<script setup lang="ts">
import Icon from "@/components/Icon.vue"
import DropdownMenuItem from "@/components/ui/dropdown-menu/DropdownMenuItem.vue"

const props = defineProps<{
    href: string
    title: string
    description: string
    icon: string
    iconSize?: string | null
}>()
</script>

<template>
    <DropdownMenuItem class="user-menu-item">
        <router-link :to="href">
            <div class="icon-section">
                <Icon :icon :size="props.iconSize" />
            </div>

            <div class="texts">
                <div class="title">{{ props.title }}</div>
                <div class="description">{{ props.description }}</div>
            </div>
        </router-link>
    </DropdownMenuItem>
</template>

<style>
.user-menu-item {
    padding: 0.7rem;

    a {
        display: flex;
        flex-direction: row;
        gap: 0.7rem;

        .icon-section {
            flex: 0;
        }

        .texts {
            flex: 1;

            .title {
                font-weight: 500;
                margin-bottom: 0.1rem;
            }

            .description {
                color: var(--muted-foreground);
            }
        }
    }
}
</style>
