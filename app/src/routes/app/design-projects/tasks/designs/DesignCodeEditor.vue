 <script setup lang="ts">
import CodeMirror from 'vue-codemirror6';
import {
  Menubar,
  MenubarCheckboxItem,
  MenubarContent,
  MenubarItem,
  MenubarMenu,
  MenubarRadioGroup,
  MenubarRadioItem,
  MenubarSeparator,
  MenubarShortcut,
  MenubarSub,
  MenubarSubContent,
  MenubarSubTrigger,
  MenubarTrigger,
} from '@/components/ui/menubar'
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';
import Icon from '@/components/Icon.vue';
import { ref } from 'vue';

const props = defineProps<{
    modelValue: string
    disabled: boolean
    tabSize?: number
}>()
const emits = defineEmits<{
    (e: "update:modelValue", payload: string): void
}>()

const autosave = ref(true);

</script>

<template>
    <div :class="`design-editor design-code-editor`">
        <div class="design-code-editor-toolbar">
            <Menubar>
                <MenubarMenu>
                    <MenubarTrigger>
                        File
                    </MenubarTrigger>

                    <MenubarContent>
                        <MenubarCheckboxItem inset v-mode="autosave">
                            Autosave enabled
                        </MenubarCheckboxItem>

                        <MenubarItem inset :disabled="autosave">
                            Save

                            <MenubarShortcut>⌘T</MenubarShortcut>
                        </MenubarItem>

                        <MenubarItem inset>
                            Reset

                            <MenubarShortcut>⌘T</MenubarShortcut>
                        </MenubarItem>

                        <MenubarSeparator />

                        <MenubarItem inset>
                            Export
                        </MenubarItem>

                        <MenubarItem inset>
                            Import
                        </MenubarItem>
                    </MenubarContent>
                </MenubarMenu>

                <MenubarMenu>
                    <MenubarTrigger>
                        Edit
                    </MenubarTrigger>

                    <MenubarContent>
                        <MenubarItem>
                            Undo

                            <MenubarShortcut>⌘T</MenubarShortcut>
                        </MenubarItem>

                        <MenubarItem>
                            Redo

                            <MenubarShortcut>⌘T</MenubarShortcut>
                        </MenubarItem>
                    </MenubarContent>
                </MenubarMenu>

                <MenubarMenu>
                    <MenubarTrigger>
                        Settings
                    </MenubarTrigger>

                    <MenubarContent>
                        <MenubarItem>
                            Language
                        </MenubarItem>
                    </MenubarContent>
                </MenubarMenu>
            </Menubar>

            <ButtonGroup>
                <Button variant="outline">
                    <Icon icon="ai-arrow-counter-clockwise" />

                    Undo
                </Button>

                <Button variant="outline">
                    <Icon icon="ai-arrow-clockwise" />

                    Redo
                </Button>
            </ButtonGroup>
        </div>

        <div class="design-code-editor-editor">
            <CodeMirror
                class="code-mirror"
                v-model="props.modelValue"
                basic
                scrollIntoView
                :disabled
                :tabSize="tabSize ?? 4"
            />
        </div>
    </div>
</template>

<style>
.design-code-editor {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    overflow: hidden;

    .design-code-editor-toolbar {
        display: flex;
        flex-direction: row;
        justify-content: start;
        align-items: center;
        gap: 0.5rem;
    }

    .design-code-editor-editor {
        flex: 1;
        overflow: auto;
        border: 1px solid var(--border);
        border-radius: var(--radius-lg);

        .code-mirror {
            height: 100%;

            .cm-editor {
                height: 100%;

                .cm-scroller {
                    font-family: "JetBrains Mono", monospace;
                    font-size: 10pt;
                    font-weight: 500;
                }

                .cm-gutters {
                    color: var(--muted-foreground);
                    background-color: var(--muted-background);
                    border-right: 0;

                }
            }
        }
    }
}
</style>
