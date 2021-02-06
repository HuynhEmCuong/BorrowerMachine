<template>
  <div class="container">
    <div class="content">
      <div class="row">
        <div class="col-12">
          <!-- add -->
          <add-component @isLoad="isLoad=$event"></add-component>
          <!-- list option -->
          <category-component></category-component>

          <!-- Pannel -->
          <table-component :isLoad="isLoad"></table-component>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import AddComponent from './AddComponent';
  import CategoryComponent from './CategoryComponent';
  import TableComponent from './TableComponent'
  export default {
    components: {
      AddComponent,
      CategoryComponent,
      TableComponent
    },
    data() {
      return {
        isLoad: false
      }
    },
    created() {
      
      FadeLoadingBar();
    }
  }
</script>
<style>
</style>
