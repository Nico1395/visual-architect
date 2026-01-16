<template>
    <div class="design-task-info-item">
        <div class="design-task-info-item-name">
            <slot name="name" />
        </div>

        <div class="design-task-info-item-content">
            <slot name="content" />
        </div>
    </div>
</template>

<style>
.design-task-info-item {
    flex: none;
    display: flex;
    flex-direction: row;
    gap: 0.4rem;
    align-items: center;

    .design-task-info-item-name {
        font-size: 10pt;
        font-weight: 500;
        line-height: 0;
    }
}
</style>
