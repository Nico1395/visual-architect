<script setup lang="ts">
import SettingsViewHeader from '../SettingsViewHeader.vue';
import { useI18n } from "vue-i18n"
import { Button } from '@/components/ui/button'
import {
  Item,
  ItemActions,
  ItemContent,
  ItemDescription,
  ItemTitle,
} from '@/components/ui/item'
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter
} from '@/components/ui/dialog';
import { ref } from 'vue';
import { useProfileStore } from '@/persistence/stores/profile.store';
import { toast } from 'vue-sonner'
import http from '@/http';

const { t } = useI18n();
const showDeleteDialog = ref(false);
const profileStore = useProfileStore();

async function confirmDelete() {
    const promise = logoutAndDelete();

    toast.promise(promise, {
        loading: t('settings.account.delete.toast.loading'),
        success: t('settings.account.delete.toast.success'),
        error: t('settings.account.delete.toast.error'),
    });

    await promise;
    window.location.href = "/auth/login";
}

async function logoutAndDelete() {
    await profileStore.deleteProfile()
    await http.post("/api/auth/logout");
    localStorage.clear();
    sessionStorage.clear();
}
</script>

<template>
    <div class="account-settings">
        <div class="account-settings-header">
            <SettingsViewHeader>
                <template #settings-header>
                    {{ t('settings.account.header') }}
                </template>

                <template #settings-description>
                    {{ t('settings.account.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        <Item variant="outline" class="account-delete-card">
            <ItemContent class="account-delete-card-content">
                <ItemTitle class="account-delete-title">
                    {{ t('settings.account.delete.title') }}
                </ItemTitle>

                <ItemDescription class="account-delete-description">
                    {{ t('settings.account.delete.description') }}
                </ItemDescription>
            </ItemContent>

            <ItemActions>
                <Button variant="destructive" @click="showDeleteDialog = true">
                    {{ t('settings.account.delete.button') }}
                </Button>
            </ItemActions>
        </Item>

        <Dialog v-model:open="showDeleteDialog">
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>
                        {{ t('settings.account.delete.modal.title') }}
                    </DialogTitle>

                    <DialogDescription>
                        {{ t('settings.account.delete.modal.description') }}
                    </DialogDescription>
                </DialogHeader>
                <DialogFooter class="flex justify-end gap-2">
                    <Button variant="outline" @click="showDeleteDialog = false" :disabled="profileStore.busy">
                        {{ t('settings.account.delete.modal.cancel') }}
                    </Button>

                    <Button variant="destructive" @click="confirmDelete" :disabled="profileStore.busy">
                        {{ t('settings.account.delete.modal.confirm') }}
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    </div>
</template>

<style>
.account-settings {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .account-delete-card {
        max-width: 500px;
        border-color: rgb(from var(--destructive) r g b / 0.2);
        color: var(--destructive);
        background-color: rgb(from var(--destructive) r g b / 0.05);
        flex-direction: column;
        align-items: start;

        .account-delete-card-content {
            gap: calc(var(--spacing) * 4);

            .account-delete-title {
                font-size: 12pt;
                font-weight: 700;
            }

            .account-delete-description {
                color: inherit;
            }
        }
    }
}
</style>
