<script setup lang="ts">
import ViewMargin from '@/components/layout/ViewMargin.vue';
import Avatar from '@/components/ui/avatar/Avatar.vue';
import AvatarImage from '@/components/ui/avatar/AvatarImage.vue';
import { useProfileStore } from '@/persistence/stores/profile.store';
import avatarFallback from '@/assets/img/avatar-fallback.png'
import SettingsMenuItem from './SettingsMenuItem.vue';
import SettingsMenuSeparator from './SettingsMenuSeparator.vue';
import Icon from '@/components/Icon.vue';
import { useI18n } from "vue-i18n"
import AvatarFallback from '@/components/ui/avatar/AvatarFallback.vue';

const profileStore = useProfileStore();
const { t } = useI18n();
</script>

<template>
    <ViewMargin class="settings-layout">
        <div class="settings-header">
            <div class="settings-user-card">
                <Avatar class="settings-user-avatar">
                    <AvatarImage :src="profileStore.$state.profile?.avatarUrl || ''" />
                    <AvatarFallback class="settings-user-avatar-fallback">
                        <img :src="avatarFallback" />
                    </AvatarFallback>
                </Avatar>

                <div class="settings-user-info">
                    <div class="display-name">
                        {{ profileStore.$state.profile?.displayName }}
                    </div>

                    <div class="email">
                        {{ profileStore.$state.profile?.email }}
                    </div>
                </div>
            </div>
        </div>

        <div class="settings-body">
            <div class="settings-menu">
                <SettingsMenuItem to="/app/settings/profile">
                    <Icon icon="ai-person" />

                    <span>
                        {{ t('settings.profile.menuitem') }}
                    </span>
                </SettingsMenuItem>

                <SettingsMenuItem to="/app/settings/personalization">
                    <Icon icon="ai-pencil" />

                    <span>
                        {{ t('settings.personalization.menuitem') }}
                    </span>
                </SettingsMenuItem>

                <SettingsMenuSeparator />

                <SettingsMenuItem to="/app/settings/account">
                    <Icon icon="ai-gear" />

                    <span>
                        {{ t('settings.account.menuitem') }}
                    </span>
                </SettingsMenuItem>
            </div>

            <div class="settings-view">
                <RouterView />
            </div>
        </div>
    </ViewMargin>
</template>

<style>

.settings-layout {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .settings-header {
        flex: non;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;

        .settings-user-card {
            flex: none;
            display: flex;
            flex-direction: row;
            align-items: center;
            gap: 0.7rem;

            .settings-user-avatar {
                border: 1px solid var(--border);
                height: 60px;
                width: 60px;
                transform-origin: center;

                &:hover {
                    animation: wiggle 0.5s ease-in-out;
                }

                .settings-user-avatar-fallback {
                    > img {
                        scale: 0.5;
                    }
                }
            }

            .settings-user-info {
                height: min-content;
                line-height: 1;

                > .display-name {
                    font-weight: 700;
                    font-size: 14pt;
                }

                > .email {
                    color: var(--muted-foreground);
                    font-weight: 500;
                    font-size: 11pt;
                }
            }
        }
    }

    .settings-body {
        flex: 1;
        display: flex;
        flex-direction: row;
        gap: 3rem;

        .settings-menu {
            width: 250px;
            flex: none;
            display: flex;
            flex-direction: column;
            gap: 0.2rem;
        }

        .settings-view {
            flex: 1;
        }
    }
}
</style>
