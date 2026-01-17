<script setup lang="ts">
import Button from './ui/button/Button.vue';
import Textarea from './ui/textarea/Textarea.vue';
import { computed, nextTick, ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import Icon from './Icon.vue';
import ButtonGroup from './ui/button-group/ButtonGroup.vue';
import {
    ContextMenu,
    ContextMenuContent,
    ContextMenuSeparator,
    ContextMenuTrigger,
} from '@/components/ui/context-menu'
import MarkdownEditorContextMenuItem from './MarkdownEditorContextMenuItem.vue';
import { useModifierKey } from '@/lib/utils';

// TODO -> Actions such as bold or italic arent recognized as an input and cannot be undone using undo

const { t } = useI18n();
const { modifier } = useModifierKey();
const undoShortcut = computed(() => t('shortcuts.undo', { modifier: modifier.value }));
const redoShortcut = computed(() => t('shortcuts.redo', { modifier: modifier.value }));

const props = defineProps<{
    modelValue: string,
    editing?: boolean,
    disabled?: boolean,
    id?: string,
    class?: string,
    placeholder?: string | null,
}>()
const emits = defineEmits<{
    (e: "update:modelValue", payload: string): void
}>()

const innerValue = ref(props.modelValue)
const textareaRef = ref<{ el: HTMLTextAreaElement | null } | null>(null)

function withSelection(transform: (selected: string) => string, cursorOffset?: number) {
    const area = textareaRef.value?.el
    if (!area)
        return

    const start = area.selectionStart
    const end = area.selectionEnd
    const value = area.value

    const selected = value.slice(start, end)
    const leadingSpaces = selected.match(/^\s*/)?.[0]?.length ?? 0
    const trailingSpaces = selected.match(/\s*$/)?.[0]?.length ?? 0
    const trimmed = selected.trim()

    const replacement = transform(trimmed)

    const replaceStart = start + leadingSpaces
    const replaceEnd = end - trailingSpaces

    area.focus({ preventScroll: true })
    area.setRangeText(
        replacement,
        replaceStart,
        replaceEnd,
        'end'
    )

    innerValue.value = area.value

    nextTick(() => {
        if (cursorOffset !== undefined) {
            const pos = replaceStart + cursorOffset
            area.setSelectionRange(pos, pos)
        }
    })
}

function bold() {
    withSelection(text =>
        text.startsWith('**') && text.endsWith('**')
            ? text.slice(2, -2)
            : `**${text || ''}**`,
        2
    )
}

function italic() {
    withSelection(text =>
        text.startsWith('*') && text.endsWith('*')
            ? text.slice(1, -1)
            : `*${text || ''}*`,
        1
    )
}

function strike() {
    withSelection(text =>
        text.startsWith('~~') && text.endsWith('~~')
            ? text.slice(2, -2)
            : `~~${text || ''}~~`,
        2
    )
}

function header(level: 1 | 2 | 3) {
    withSelection(text => {
        const trimmed = text.replace(/^#{1,6}\s*/, "")
        return `${"#".repeat(level)} ${trimmed}`
    })
}

function triggerUndo() {
    const area = textareaRef.value?.el
    if (!area)
        return

    area.focus({ preventScroll: true })
    document.execCommand('undo')
}

function triggerRedo() {
    const area = textareaRef.value?.el
    if (!area)
        return

    area.focus({ preventScroll: true })
    document.execCommand('redo')
}

watch(
    () => props.modelValue,
    v => {
        if (v !== innerValue.value) {
            innerValue.value = v
        }
    }
)

watch(innerValue, v => {
    emits('update:modelValue', v)
})
</script>

<template>
    <div class="markdown-editor">
        <div class="markdown-editor-header">
            <div class="markdown-editor-header-actions">
                <slot name="buttons" />

                <ButtonGroup>
                    <Button size="icon-sm" variant="outline" @click="triggerUndo">
                        <Icon icon="ai-arrow-counter-clockwise" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="triggerRedo">
                        <Icon icon="ai-arrow-clockwise" />
                    </Button>
                </ButtonGroup>

                <ButtonGroup>
                    <Button size="icon-sm" variant="outline" @click="() => header(1)">
                        <Icon icon="bi bi-type-h1" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="() => header(2)">
                        <Icon icon="bi bi-type-h2" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="() => header(3)">
                        <Icon icon="bi bi-type-h3" />
                    </Button>
                </ButtonGroup>

                <ButtonGroup>
                    <Button size="icon-sm" variant="outline" @click="bold">
                        <Icon icon="bi bi-type-bold" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="italic">
                        <Icon icon="bi bi-type-italic" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="strike">
                        <Icon icon="bi bi-type-strikethrough" />
                    </Button>
                </ButtonGroup>
            </div>

            <div class="markdown-editor-header-title">
                <slot name="title" />
            </div>
        </div>

        <div class="markdown-editor-body">
            <ContextMenu>
                <ContextMenuTrigger class="markdown-editor-context-menu-trigger">
                    <Textarea
                        ref="textareaRef"
                        class="markdown-editor-textarea"
                        :disabled
                        :id
                        :placeholder
                        v-model="innerValue"
                    />
                </ContextMenuTrigger>

                <ContextMenuContent>
                    <MarkdownEditorContextMenuItem icon="bi bi-type-bold" @click="bold">
                        <template #title>
                            {{ t('components.markdownArea.bold') }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <MarkdownEditorContextMenuItem icon="bi bi-type-italic" @click="italic">
                        <template #title>
                            {{ t('components.markdownArea.italic') }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <MarkdownEditorContextMenuItem icon="bi bi-type-strikethrough" @click="strike">
                        <template #title>
                            {{ t('components.markdownArea.strikethrough') }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <ContextMenuSeparator />

                    <MarkdownEditorContextMenuItem icon="bi bi-type-h1" @click="header(1)">
                        <template #title>
                            {{ t('components.markdownArea.header1') }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <MarkdownEditorContextMenuItem icon="bi bi-type-h2" @click="header(2)">
                        <template #title>
                            {{ t('components.markdownArea.header2') }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <MarkdownEditorContextMenuItem icon="bi bi-type-h3" @click="header(3)">
                        <template #title>
                            {{ t('components.markdownArea.header3') }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <ContextMenuSeparator />

                    <MarkdownEditorContextMenuItem icon="ai-arrow-counter-clockwise" @click="triggerUndo">
                        <template #title>
                            {{ t('components.markdownArea.undo') }}
                        </template>

                        <template #shortcut>
                            {{ undoShortcut }}
                        </template>
                    </MarkdownEditorContextMenuItem>

                    <MarkdownEditorContextMenuItem icon="ai-arrow-clockwise" @click="triggerRedo">
                        <template #title>
                            {{ t('components.markdownArea.redo') }}
                        </template>

                        <template #shortcut>
                            {{ redoShortcut }}
                        </template>
                    </MarkdownEditorContextMenuItem>
                </ContextMenuContent>
            </ContextMenu>
        </div>
    </div>
</template>

<style>
.markdown-editor {
    display: flex;
    flex-direction: column;
    border-radius: var(--radius-md);
    border: 1px solid var(--border);
    min-height: 300px;

    .markdown-editor-header {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        padding: 0.3rem;
        border-radius: var(--radius-xl) var(--radius-xl) 0 0;
        border-bottom: 1px solid var(--border);
        position: sticky;
        top: 0;
        z-index: 10;
        background-color: var(--background);

        .markdown-editor-header-title {
            flex: none;
            font-size: 10pt;
            font-weight: 500;
            color: var(--muted-foreground);
            margin-right: 0.5rem;
        }

        .markdown-editor-header-actions {
            flex: none;
            display: flex;
            flex-direction: row;
            gap: 0.3rem;
        }
    }

    .markdown-editor-body {
        flex: 1;
        display: flex;
        flex-direction: column;
        position: relative;

        .markdown-editor-context-menu-trigger {
            flex: 1;
            display: flex;
            flex-direction: column;

            .markdown-editor-textarea {
                flex: 1;
                font-family: "JetBrains Mono";
                font-weight: 500;
                font-size: 10pt;
                resize: none;
                padding: 0.5rem;
                overflow-y: auto;
                border-radius: 0 0 var(--radius-xl) var(--radius-xl);
                border: none;
                background-color: var(--background);

                &::placeholder {
                    font-weight: 400;
                }

                &::selection {
                    background-color:var(--selection);
                }
            }
        }
    }
}
</style>
