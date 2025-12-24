<script setup lang="ts">
import ViewMargin from '@/components/layout/ViewMargin.vue';
import Avatar from '@/components/ui/avatar/Avatar.vue';
import AvatarImage from '@/components/ui/avatar/AvatarImage.vue';
import { useProfileStore } from '@/persistence/stores/profile.store';
import avatarFallback from '@/assets/img/avatar-fallback.png'
import PreferencesMenuItem from './PreferencesMenuItem.vue';
import PreferencesMenuSeparator from './PreferencesMenuSeparator.vue';
import Icon from '@/components/Icon.vue';
import { useI18n } from "vue-i18n"
import AvatarFallback from '@/components/ui/avatar/AvatarFallback.vue';

const profileStore = useProfileStore();
const { t } = useI18n();
</script>

<template>
    <ViewMargin class="preferences-layout">
        <div class="preferences-header">
            <div class="preferences-user-card">
                <Avatar class="preferences-user-avatar">
                    <AvatarImage :src="profileStore.$state.profile?.avatarUrl || ''" />
                    <AvatarFallback class="preferences-user-avatar-fallback">
                        <img :src="avatarFallback" />
                    </AvatarFallback>
                </Avatar>

                <div class="preferences-user-info">
                    <div class="display-name">
                        {{ profileStore.$state.profile?.displayName }}
                    </div>

                    <div class="email">
                        {{ profileStore.$state.profile?.email }}
                    </div>
                </div>
            </div>
        </div>

        <div class="preferences-body">
            <div class="preferences-menu">
                <PreferencesMenuItem to="/app/preferences/profile">
                    <Icon icon="ai-person" />

                    <span>
                        {{ t('preferences.profile.menuitem') }}
                    </span>
                </PreferencesMenuItem>

                <PreferencesMenuItem to="/app/preferences/personalization">
                    <Icon icon="ai-pencil" />

                    <span>
                        {{ t('preferences.personalization.menuitem') }}
                    </span>
                </PreferencesMenuItem>

                <PreferencesMenuSeparator />

                <PreferencesMenuItem to="/app/preferences/account">
                    <Icon icon="ai-gear" />

                    <span>
                        {{ t('preferences.account.menuitem') }}
                    </span>
                </PreferencesMenuItem>
            </div>

            <div class="preferences-view">
                <RouterView />
            </div>
        </div>
    </ViewMargin>
</template>

<style>
.preferences-layout {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .preferences-header {
        flex: non;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;

        .preferences-user-card {
            flex: none;
            display: flex;
            flex-direction: row;
            align-items: center;
            gap: 0.7rem;

            .preferences-user-avatar {
                border: 1px solid var(--border);
                height: 60px;
                width: 60px;
                transform-origin: center;

                &:hover {
                    animation: wiggle 0.5s ease-in-out;
                }

                .preferences-user-avatar-fallback {
                    > img {
                        scale: 0.5;
                    }
                }
            }

            .preferences-user-info {
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

    .preferences-body {
        flex: 1;
        display: flex;
        flex-direction: row;
        gap: 3rem;

        .preferences-menu {
            width: 250px;
            flex: none;
            display: flex;
            flex-direction: column;
            gap: 0.2rem;
        }

        .preferences-view {
            flex: 1;
        }
    }
}
</style>
