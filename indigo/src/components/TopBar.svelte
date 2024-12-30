<script>
    import "../css/main.css"
    import Icon from "@iconify/svelte"
    import { selectedPlaylist } from "../store";
    import { getSongs } from "$lib";

    let link = "";

    async function downloadSong() {
        let p = null;
        if($selectedPlaylist) {
            p = $selectedPlaylist;
        }

        await fetch("http://localhost:24896/download", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                url: link,
                playlist: p
            })
        });

        getSongs();
    }
</script>

<div class="top-bar">
    <div class="centered-side">
        <input id="download-input" class="top-input" placeholder="Enter a Youtube Link!" bind:value={link} />
        <div on:click={async () => await downloadSong()}>
            <Icon icon="line-md:downloading-loop" width="2em" height="2em"  style="color: #1b9303" />
        </div>
    </div>
</div>