<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';
import {
    DropdownMenu, DropdownMenuContent, DropdownMenuGroup, DropdownMenuItem, DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import Input from '@/components/ui/input/Input.vue';
import { useI18n } from "vue-i18n"

const { t } = useI18n();
</script>

<template>
    <ButtonGroup>
        <!-- Time span -->
        <!-- Status -->

        <Input type="text" :placeholder="t('designprojects.tasks.filters.search')" />

        <DropdownMenu>
            <DropdownMenuTrigger as-child>
                <Button variant="outline" size="icon">
                    <Icon icon="ai-sort" />
                </Button>
            </DropdownMenuTrigger>

            <DropdownMenuContent align="end" class="[--radius:1rem]">
                <DropdownMenuGroup>
                    <DropdownMenuItem>
                        <Icon icon="bi bi-calendar2-date" />

                        Created Ascending
                    </DropdownMenuItem>

                    <DropdownMenuItem>
                        <Icon icon="bi bi-calendar2-date" />

                        Created Descending
                    </DropdownMenuItem>

                    <DropdownMenuItem>
                        <Icon icon="bi bi-three-dots" />

                        Status Ascending
                    </DropdownMenuItem>

                    <DropdownMenuItem>
                        <Icon icon="bi bi-three-dots" />

                        Status Descending
                    </DropdownMenuItem>
                </DropdownMenuGroup>
            </DropdownMenuContent>
        </DropdownMenu>
    </ButtonGroup>
</template>

