<script setup lang="ts">
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import UserMenuItem from "./UserMenuItem.vue"
import { useI18n } from "vue-i18n"
import Separator from "@/components/ui/separator/Separator.vue"

const { t } = useI18n()
</script>

<template>
    <DropdownMenu>
        <DropdownMenuTrigger as-child>
            <div class="user-menu">
                <Avatar class="user-avatar">
                    <AvatarImage src="" />
                    <AvatarFallback class="user-avatar-fallback"> UN </AvatarFallback>
                </Avatar>
            </div>
        </DropdownMenuTrigger>

        <DropdownMenuContent align="end" :side-offset="8">
            <div class="user-menu-content">
                <UserMenuItem
                    href="/app/settings"
                    icon="ai-gear"
                    :title="t('layout.header.usermenu.settings')"
                    :description="t('layout.header.usermenu.settingsdesc')"
                />

                <Separator />

                <UserMenuItem
                    href="/auth/logout"
                    icon="ai-door"
                    :title="t('layout.header.usermenu.logout')"
                    :description="t('layout.header.usermenu.logoutdesc')"
                />
            </div>
        </DropdownMenuContent>
    </DropdownMenu>
</template>

<style scoped>
.user-menu {
    height: 40px;
    width: 40px;
    border-radius: 20px;
    border: 1px solid var(--border);
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;

    .user-avatar-fallback {
        background-color: transparent;
    }
}

.user-menu-content {
    display: flex;
    flex-direction: column;
    gap: 0.3rem;
    padding: 0.2rem;
}
</style>
