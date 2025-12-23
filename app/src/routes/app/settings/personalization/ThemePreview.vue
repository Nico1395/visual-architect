<script setup lang="ts">
import { Button } from '@/components/ui/button'

const props = defineProps<{
    theme?: string | null
    disabled?: boolean
}>()
const emits = defineEmits<{
    (e: "click"): void,
}>()

function onClick() {
    emits("click")
}
</script>

<template>
    <Button class="theme-preview" variant="outline" @click="onClick()" :disabled>
        <div :class="`theme-example ${props.theme ?? ''}`.trim()">
            <div class="header">
                <div class="logo-skeletton"></div>

                <div class="avatar-skeletton"></div>
            </div>

            <div class="body">
                <div class="content">
                    <div class="section">
                        <div class="heading-skeletton"></div>

                        <div class="boxes">
                            <div class="box-skeletton"></div>
                            <div class="box-skeletton"></div>
                            <div class="box-skeletton"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="theme-name">
            <slot />
        </div>
    </Button>
</template>

<style>
.theme-preview {
    flex: 1;
    height: 200px;
    display: flex;
    flex-direction: column;
    justify-content: unset;
    align-items: unset;
    padding: 1rem;
    overflow: hidden;

    .theme-example {
        overflow: hidden;
        display: flex;
        flex-direction: column;
        border-radius: var(--radius-md);
        border: 1px solid var(--border);
        flex: 1;
        background-color: var(--background);

        > .header {
            background-color: var(--sidebar);
            height: 30px;
            border-bottom: 1px solid var(--border);
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            align-items: center;
            padding: 0 0.3rem;

            > .logo-skeletton {
                background-color: var(--muted);
                flex: none;
                height: 15px;
                width: 50px;
                border-radius: 10px;
            }

            > .avatar-skeletton {
                flex: none;
                background-color: var(--muted);
                height: 20px;
                width: 20px;
                border-radius: 10px;
            }
        }

        > .body {
            padding: 1rem 0;
            height: 100%;

            > .content {
                margin: 0 3rem;
                height: 100%;

                .section {
                    text-align: start;

                    .heading-skeletton {
                        height: 5px;
                        width: 50px;
                        border-radius: 5px;
                        background-color: var(--muted);
                        margin-bottom: 0.3rem;
                    }

                    .boxes {
                        display: flex;
                        flex-direction: row;
                        gap: 0.2rem;

                        .box-skeletton {
                            width: 30px;
                            height: 30px;
                            border-radius: 5px;
                            background-color: var(--muted);
                        }
                    }
                }
            }
        }
    }

    .theme-name {
        flex: none;
        font-weight: 600;
    }
}
</style>
