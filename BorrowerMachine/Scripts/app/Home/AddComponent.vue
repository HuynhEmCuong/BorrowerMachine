<template>
  <div>
    <div class="button-add">
      <button class="btn btn-success" data-toggle="modal" data-target=".modalAdd">
        <i class="fa fa-plus-circle"></i>
        {{$t('homepage.Borrow_Device')}}
      </button>
    </div>
    <!-- Modal add -->
    <modal-add-borrower-component></modal-add-borrower-component>
  </div>
</template>
<script>
  import ModalAddBorrowerComponent from './ModalAddBorrowerComponent'
  export default {
    components: {
      ModalAddBorrowerComponent
    }
  }

</script>
