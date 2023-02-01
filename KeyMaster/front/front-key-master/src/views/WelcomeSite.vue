<template>
  <Navbar></Navbar>
  <div class="MainSiteContent">
    <div class="WelcomeContent">
      <h1 class="titles">Jak się zalogowac?</h1>
      <h2>Aby się zalogować należy kliknąć w przycisk zaloguj się.</h2>
      <p>
        <v-img src="../assets/logowanie1.jpg" alt="klawiatura" />
      </p>
      <h2>Nstępnym krokiem będzie logowanie poprzez formularz.</h2>
      <p>
        <v-img src="../assets/logowanie2.jpg" alt="klawiatura" />
      </p>
      <h2>
        Po zalogowaniu strona przeniesię cię spowrotem na główną stronę i przy
        przycisku wyloguj się pojawi się twój nickname.
      </h2>
      <p>
        <v-img src="../assets/logowanie3.jpg" alt="klawiatura" />
      </p>
      <h2>
        Jeśli chcesz się zajestrować w panelu logowania kliknij przycisk
        rejesracja.
      </h2>
      <p>
        <v-img src="../assets/logowanie4.jpg" alt="klawiatura" />
      </p>
      <h2>Następnie wypełnić formulaż i kliknąć przycisk rejestracja.</h2>
      <p>
        <v-img src="../assets/logowanie5.jpg" alt="klawiatura" />
      </p>
      <h2>
        Przed ostatnim krokiem jest potwierdzenie adresu email. Aby to zrobić
        sprawdź swoją skrzynkę pocztową i kliknij na przycisk mówiący o
        weryfikacji email.
      </h2>
      <h1>Jak korzystać z aplikacji?</h1>

      <h2>
        Poniżej jest widoczny widok jak wyglądają zakładki ćwiczeń. Wszystkie są
        dostępne bez konieczności logowania.
      </h2>
      <p>
        <v-img src="../assets/klawiatura.jpg" alt="klawiatura" />
      </p>
      <h2>
        Aby kożystać z lekcji kliknij na przycisk Lekcji, poniżej pojawią się
        lekcje do wybania.
      </h2>
      <p>
        <v-img src="../assets/lekcje.jpg" alt="lekcja" />
      </p>
      <h2>
        Lekcje są ponumerowane nie znaczy to że musisz przechodzić je po kolei
        możesz wybrać dowolną. Litery pod numerem lekcji to temat czyli jakie
        litery będą używane w ćwiczeniu.
      </h2>
      <h2 class="mt-6 mb-6">
        Tylko lekcje możesz sam wybierać. Ćwiczenia i testy szybkości są
        losowane z pewnej puli tekstów.
      </h2>
    </div>
  </div>
  <Footer></Footer>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";
import { defineComponent } from "vue";

export default defineComponent({
  name: "WelcomeSite",
  components: {
    Navbar,
    Footer,
  },
});
</script>

<style scoped></style>
