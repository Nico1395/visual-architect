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

const { t } = useI18n();
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
                <Button variant="destructive" >
                    {{ t('settings.account.delete.button') }}
                </Button>
            </ItemActions>
        </Item>
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
