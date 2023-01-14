<template>
    <NavbarVue></NavbarVue>
      <div class="test-title">
        <h1>
        Witaj na stronie testowej.
      </h1>
      <p>
        Tutaj będziesz mógł/mogła sprawdzić swoje postępy w praktycznym teście.
      </p>
      </div>
        <Content></Content>
  <FooterVue></FooterVue>
  </template>
  
  <script lang='ts'>
  import { defineComponent } from 'vue'
  import NavbarVue from '../components/Navbar.vue';
  import Content from '@/components/Content.vue';
  import FooterVue from '../components/Footer.vue';
  
  export default defineComponent({
    name: 'Lesson',
    components: {
      NavbarVue,
      FooterVue,
      Content
  },
  
    })
  </script>
  