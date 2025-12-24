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
import { useProfileStore } from "@/persistence/stores/profile.store"
import { onMounted } from "vue";
import avatarFallback from '@/assets/img/avatar-fallback.png'

const { t } = useI18n()
const router = useRouter();
const profileStore = useProfileStore();

onMounted(async () => {
    await profileStore.getProfile();
})

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
                <Avatar>
                    <AvatarImage :src="profileStore.$state.profile?.avatarUrl || ''" />
                    <AvatarFallback class="user-avatar-fallback">
                        <img :src="avatarFallback" />
                    </AvatarFallback>
                </Avatar>
            </div>
        </DropdownMenuTrigger>

        <DropdownMenuContent align="end" :side-offset="8">
            <div class="user-menu-content">
                <div class="user-menu-info">
                    {{ t('layout.header.usermenu.signedinas') }}

                    <span>
                        {{ profileStore.$state.profile?.displayName }}
                    </span>
                </div>

                <Separator />

                <UserMenuItemLink
                    href="/app/preferences/profile"
                    icon="ai-gear"
                    :title="t('layout.header.usermenu.preferences')"
                    :description="t('layout.header.usermenu.preferencesdesc')"
                />

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

        &:hover {
            animation: wiggle 0.5s ease-in-out;
        }

        > img {
            scale: 0.5;
        }
    }
}

.user-menu-content {
    display: flex;
    flex-direction: column;
    gap: 0.3rem;
    padding: 0.2rem;

    .user-menu-info {
        text-align: center;
        white-space: wrap;
        padding: 0.7rem;
        font-weight: 500;
        font-size: 11pt;
        color: var(--muted-foreground);

        > span {
            color: var(--foreground);
        }
    }
}
</style>
