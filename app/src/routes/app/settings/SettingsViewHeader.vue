<template>
    <div class="settings-view-header">
        <h1>
            <slot name="settings-header" />
        </h1>

        <p>
            <slot name="settings-description" />
        </p>
    </div>
</template>

<style>
.settings-view-header {
    flex: none;
    margin-bottom: 1rem;

    > h1 {
        font-weight: 700;
        font-size: 20pt;
        margin-bottom: 0.4rem;
    }

    > p {
        font-size: 11pt;
        font-weight: 500;
        color: var(--muted-foreground);
    }
}
</style>
