<script setup lang="ts">
import ButtonGroup from './ui/button-group/ButtonGroup.vue';
import Button from './ui/button/Button.vue';
import VueMarkdown from 'vue-markdown-render'
import { computed, ref } from 'vue';
import { useI18n } from 'vue-i18n';
import Icon from './Icon.vue';
import MarkdownEditor from './MarkdownEditor.vue';

const { t } = useI18n();
const props = defineProps<{
    modelValue: string,
    isPreviewing?: boolean,
    disabled?: boolean,
    id?: string,
    class?: string,
}>()
const emits = defineEmits<{
    (e: "update:modelValue", payload: string): void
}>()
const isPreviewing = ref(props.isPreviewing ?? false)
const value = computed({
  get: () => props.modelValue,
  set: (val: string) => emits('update:modelValue', val),
})

function togglePreview(preview: boolean) {
    isPreviewing.value = preview;
}
</script>

<template>
    <div :class="`previewable-markdown-editor ${props.class}`.trim()">
        <div v-if="isPreviewing" class="previewable-markdown-editor-actions-wrapper">
            <ButtonGroup class="previewable-markdown-editor-actions">
                <Button :disabled @click="togglePreview(false)" :variant="isPreviewing ? 'outline' : 'default'" type="button" size="sm">
                    <Icon icon="bi bi-markdown" size="16px" />

                    {{ t('components.markdownEditor.write') }}
                </Button>

                <Button :disabled @click="togglePreview(true)" :variant="isPreviewing ? 'default' : 'outline'" type="button"  size="sm">
                    <Icon icon="bi bi-justify-left" size="16px" />

                    {{ t('components.markdownEditor.preview') }}
                </Button>
            </ButtonGroup>
        </div>

        <div v-if="!isPreviewing" class="previewable-markdown-editor-body">
            <MarkdownEditor :disabled :id :placeholder="t('components.markdownEditor.placeholder')" v-model="value">
                <template #title>
                    <ButtonGroup>
                        <Button :disabled @click="togglePreview(false)" :variant="isPreviewing ? 'outline' : 'default'" type="button" size="sm">
                            <Icon icon="bi bi-markdown" size="16px" />

                            {{ t('components.markdownEditor.write') }}
                        </Button>

                        <Button :disabled @click="togglePreview(true)" :variant="isPreviewing ? 'default' : 'outline'" type="button"  size="sm">
                            <Icon icon="bi bi-justify-left" size="16px" />

                            {{ t('components.markdownEditor.preview') }}
                        </Button>
                    </ButtonGroup>
                </template>
            </MarkdownEditor>
        </div>

        <div v-else class="previewable-markdown-editor-body">
            <VueMarkdown :disabled class="markdown" :source="value" />
        </div>
    </div>

</template>

<style>
.previewable-markdown-editor {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    position: relative;

    .previewable-markdown-editor-actions-wrapper {
        height: 100%;
        position: absolute;
        top: 0;
        right: 0;
        width: 100%;
        pointer-events: none;
        padding: 0.5rem;

        .previewable-markdown-editor-actions {
            position: sticky;
            top: 0.5rem;
            float: right;
            z-index: 10;
            background-color: var(--background);
            pointer-events: auto;
            display: inline-flex;
        }
    }

    .previewable-markdown-editor-body {
        height: 300px;
        border-radius: var(--radius-md);
        border: 1px solid var(--border);

        .markdown-editor {
            border: none;

            .markdown-editor-header-title {
                margin: 0;
            }
        }

        > * {
            height: 100%;
        }

        > .markdown {
            cursor: default;
            font-size: 10pt;
            padding: 0.5rem 0.7rem;
            overflow-y: auto;
        }
    }
}

.markdown {
  line-height: 1.7;
  color: inherit;
}

/* Headings */
.markdown h1 {
  font-size: 1.7rem;
  font-weight: 700;
  margin: 0.7rem 0;
}

.markdown h2 {
  font-size: 1.35rem;
  font-weight: 600;
  margin: 0.55rem 0 0.55rem;
}

.markdown h3 {
  font-size: 1rem;
  font-weight: 600;
  margin: 0.3rem 0 0.3rem;
}

.markdown p {
  margin: 0.65rem 0;
}

.markdown ul,
.markdown ol {
  padding-left: 1.5rem;
  margin: 0.75rem 0;
}

.markdown ul {
  list-style: disc;
}

.markdown ol {
  list-style: decimal;
}

.markdown code {
  font-family: "JetBrains Mono", monospace;
  background-color: var(--muted-background);
  padding: 0.15rem 0.35rem;
  border-radius: 4px;
}

.markdown pre {
  background-color: var(--muted-background);
  padding: 1rem;
  border-radius: 6px;
  overflow-x: auto;
}

.markdown pre code {
  background: none;
  padding: 0;
}

.markdown a {
    color: var(--link);
    text-decoration: 1px underline currentColor;

    &:visited {
        color: var(--visited-link);
    }
}
</style>
