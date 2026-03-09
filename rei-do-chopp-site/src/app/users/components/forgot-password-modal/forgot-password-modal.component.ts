import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UsersService } from '../../services/users.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UserForgotPasswordRequest } from '../../models/requests/user-forgot-password.request';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormInvalidMessageComponent } from "../../../shared/components/form-invalid-message/form-invalid-message.component";

@Component({
  selector: 'app-forgot-password-modal',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FormInvalidMessageComponent
],
  templateUrl: './forgot-password-modal.component.html'
})
export class ForgotPasswordModalComponent {
  constructor(
    private readonly modalRef: BsModalRef,
    private readonly userService: UsersService,
    private readonly formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private readonly toastr: ToastrService
  ) { }

  form: FormGroup;

  ngOnInit(): void {
    this.form = this.formBuilder.group({'Email': ['', [Validators.required, Validators.email]]});
  }

  public closeModal() {
    this.modalRef.hide();
  }

  public send() {
    this.spinner.show()

    const request = new UserForgotPasswordRequest({ ...this.form.value, RoleIds: [this.form.value.RoleIds] })

    this.userService.forgotPassword(request)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe({
        next: () => {
          this.closeModal();
          this.toastr.success("Verifique seu email")
        }
      });
  }
}
