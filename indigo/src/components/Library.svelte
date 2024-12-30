<script>
    import "../css/main.css"
    import Icon from "@iconify/svelte"
    import { playlists } from "../store";
    import Playlist from "./Playlist.svelte";
</script>

<div class="library">
    <div class="centered-down">
        {#each $playlists as p}
            <Playlist playlist={p}></Playlist>
        {/each}
    </div>
</div>