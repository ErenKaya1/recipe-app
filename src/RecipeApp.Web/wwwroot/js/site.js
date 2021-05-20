$(document).ready(() => {
  $(".delete-moderator").click(function () {
    Swal.fire({
      title: "Emin misiniz?",
      text: "Moderatör silinecektir.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Evet",
      cancelButtonText: "Vazgeç",
    }).then((result) => {
      if (result.isConfirmed) {
        const id = $(this).attr("data-id");
        $.ajax(`/moderator/delete?moderatorId=${id}`, {
          success: () => {
            toastr.info("Başarıyla silindi.");
            $(`[data-row=${id}]`).remove();
          },

          error: (response) => {
            switch (response.status) {
              case 404:
                toastr.error("Kullanıcı bulunamadı.");
                break;

              default:
                toastr.error("Bir hata oluştu.");
                break;
            }
          },
        });
      }
    });
  });

  $("input[name='OldPassword']").on("input", function () {
    if ($(this).val()) {
      $("input[name='NewPassword']").prop("disabled", false);
      $("input[name='NewPasswordConfirm']").prop("disabled", false);
    } else {
      $("input[name='NewPassword']").prop("disabled", true);
      $("input[name='NewPasswordConfirm']").prop("disabled", true);
    }
  });

  $(".update-profile-form").on("submit", function (e) {
    e.preventDefault();
    $.ajax("/user/updateprofile", {
      type: "POST",
      data: $(this).serialize(),

      success: function () {
        $(".current-user").text($("input[name='Username']").val());
        toastr.success("Hesap başarıyla güncellendi");
      },

      error: function (response) {
        switch (response.status) {
          case 404:
            toastr.error("Kullanıcı bulunamadı.");
            break;

          default:
            toastr.error("Hesap güncellenemedi.");
            break;
        }
      },
    });
  });

  $("input[name='CurrentPassword']").on("input", function () {
    if ($(this).val()) {
      $("input[name='NewPassword']").prop("disabled", false);
      $("input[name='NewPasswordConfirm']").prop("disabled", false);
    } else {
      $("input[name='NewPassword']").prop("disabled", true);
      $("input[name='NewPasswordConfirm']").prop("disabled", true);
    }
  });

  $(".change-password-form").on("submit", function (e) {
    e.preventDefault();

    $.ajax("/user/changepassword", {
      type: "POST",
      data: $(this).serialize(),

      success: function () {
        toastr.success("Parola başarıyla güncellendi.");
      },

      error: function (response) {
        switch (response.status) {
          case 404:
            toastr.error("Kullanıcı bulunamadı.");
            break;

          default:
            toastr.error("Lütfen bilgileri kontrol ediniz.");
            break;
        }
      },
    });
  });

  $(".edit-category-form").on("submit", function (e) {
    e.preventDefault();

    $.ajax("/category/edit", {
      type: "POST",
      data: new FormData(this),
      contentType: false,
      processData: false,

      success: function () {
        $("input[name='ImageFile']").val("");
        toastr.success("Başarıyla güncellendi.");
      },

      error: function (response) {
        console.log(response);
        toastr.error(response.responseJSON.errorMessage);
      },
    });
  });

  $(".delete-category").on("click", function () {
    Swal.fire({
      title: "Emin misiniz?",
      text: "Kategori ve bu kategorideki tüm tarifler silinecektir.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Evet",
      cancelButtonText: "Vazgeç",
    }).then((result) => {
      if (result.isConfirmed) {
        const id = $(this).attr("data-id");
        $.ajax(`/category/delete?categoryId=${id}`, {
          success: function () {
            toastr.info("Başarıyla silindi.");
            $(`[data-row=${id}]`).remove();
          },

          error: function () {
            toastr.error("Kategori bulunamadı.");
          },
        });
      }
    });
  });

  $("input[name='ImageFile']").on("input", function () {
    const imageUrl = URL.createObjectURL($(this)[0].files[0]);
    $(".img-preview").prop("src", imageUrl);
    if ($(".img-preview-container").hasClass("d-none")) {
      $(".img-preview-container").removeClass("d-none").addClass("d-block");
    }
  });

  $(".edit-recipe-form").on("submit", function (e) {
    e.preventDefault();

    $.ajax("/recipe/edit", {
      type: "POST",
      data: new FormData(this),
      processData: false,
      contentType: false,

      success: function () {
        $("input[name='ImageFile']").val("");
        toastr.success("Tarif başarıyla güncellendi.");
      },

      error: function () {
        toastr.error("Tarif güncellenemedi.");
      },
    });
  });

  $(".delete-recipe").on("click", function () {
    Swal.fire({
      title: "Emin misiniz?",
      text: "Tarif silinecektir.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Evet",
      cancelButtonText: "Vazgeç",
    }).then((result) => {
      if (result.isConfirmed) {
        const id = $(this).attr("data-id");

        $.ajax(`/recipe/delete?recipeId=${id}`, {
          success: function () {
            toastr.info("Başarıyla silindi.");
            $(`[data-row=${id}]`).remove();
          },

          error: function () {
            toastr.error("Tarif bulunamadı.");
          },
        });
      }
    });
  });
});
