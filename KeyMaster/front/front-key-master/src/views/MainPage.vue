<template>
   <Navbar></Navbar>     
   <Content></Content>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Navbar from "@/components/Navbar.vue";
import Content from "@/components/Content.vue";

@Options({
  components: {
    Navbar,
    Content,
  },
  })
export default class MainPage extends Vue {}
</script>
