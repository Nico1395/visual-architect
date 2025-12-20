<script setup lang="ts">
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import UserMenuItemLink from "./UserMenuItemLink.vue"
import UserMenuItemButton from "./UserMenuItemButton.vue"
import { useI18n } from "vue-i18n"
import Separator from "@/components/ui/separator/Separator.vue"
import http from "@/http";
import { useRouter } from "vue-router"

const { t } = useI18n()
const router = useRouter();

async function logout() {
  try {
    await http.post("/api/auth/logout");
  } finally {
    sessionStorage.removeItem("vat");
    router.replace("/auth/login");
  }
}
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
                <UserMenuItemLink
                    href="/app/settings"
                    icon="ai-gear"
                    :title="t('layout.header.usermenu.settings')"
                    :description="t('layout.header.usermenu.settingsdesc')"
                />

                <Separator />

                <UserMenuItemButton
                    @click="logout"
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
