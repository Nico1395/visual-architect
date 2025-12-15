<script lang="ts">
    import "./+page.scss";
    import * as Card from "$lib/components/ui/card/index";
    import logo_64x64 from "$lib/assets/visual-architect-logo_64x64.png";
    import { _ } from "svelte-i18n";
    import Button from "$lib/components/ui/button/button.svelte";
    import Icon from "$lib/components/ui/Icon.svelte";

    function getSplashTextKey(index: number) {
        return `auth.login.calltoaction.[${index}]`;
    }

    let splashTextKey = $state("");
    function toggleSplashText() {
        const splashTextKeyIndex = parseInt(localStorage.getItem("splash_index") ?? "0");
        splashTextKey = getSplashTextKey(splashTextKeyIndex);

        const nextIndex = splashTextKeyIndex >= 7 ? 0 : splashTextKeyIndex + 1;
        localStorage.setItem("splash_index", nextIndex.toString());
    }

    toggleSplashText();
</script>

<Card.Root class="login-card">
    <Card.Header class="login-header">
        <img src={logo_64x64} alt={logo_64x64} />

        <h1>{$_("common.app.name")}</h1>
    </Card.Header>

    <Card.Content class="login-content">
        <Button class="login-splash-text" onclick={() => toggleSplashText()} variant="ghost">
            {$_(splashTextKey)}
        </Button>
    </Card.Content>

    <Card.Footer class="login-footer">
        <Button class="login-footer-github" variant="github">
            <Icon icon="ai-github-fill" />
            
            {$_("auth.login.github")}
        </Button>
    </Card.Footer>
</Card.Root>
