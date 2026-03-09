import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, input, OnInit, Output, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserResponse } from '../../models/responses/user.response';
import { UsersService } from '../../services/users.service';
import { NgxMaskDirective } from 'ngx-mask';
import { InputFocusDirective } from '../../../shared/directives/input-focus.directive';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';
import { FormInvalidMessageComponent } from "../../../shared/components/form-invalid-message/form-invalid-message.component";
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
import { IntegerInputComponent } from '../../../shared/components/inputs/integer-input/integer-input.component';
import { UserRegisterRequest } from '../../models/requests/user-register-request';
import { PasswordValidator } from '../../validators/password.validator';
import { PasswordMatchValidator } from '../../validators/password-match.validator';
import { RolesSelectComponent } from "../../../roles/components/roles-select/roles-select.component";
import { UserEditRequest } from '../../models/requests/user-edit-request';
import { ToastrService } from 'ngx-toastr';
import { USER_EDIT_FORM_CONFIG } from '../../forms/user-edit-form.config';
import { USER_REGISTER_FORM_CONFIG } from '../../forms/user-register-form.config';

@Component({
  selector: 'app-user-register-modal',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMaskDirective,
    InputFocusDirective,
    FormControlDirective,
    FormInvalidMessageComponent,
    IntegerInputComponent,
    RolesSelectComponent
],
  templateUrl: './user-register-modal.component.html',
})
export class UserRegisterModalComponent implements OnInit {

  @Input() user?: UserResponse
  @Output('save') saveEmitter = new EventEmitter();

  form: FormGroup;
  passwordInputType = "password"

  constructor(
    private readonly modalRef: BsModalRef,
    private readonly userService: UsersService,
    private readonly formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private readonly toastr: ToastrService
) { }

  ngOnInit(): void {
    const formConfig = this.user ? USER_EDIT_FORM_CONFIG : USER_REGISTER_FORM_CONFIG;

    this.form = this.formBuilder.group(formConfig, {validators: PasswordMatchValidator('Password', 'CheckPassword')});

    if(this.user)
      this.form.patchValue({...this.user, RoleIds: this.user.Roles[0].Id})

  }

  public closeModal() {
    this.modalRef.hide();
  }

  public salvar() {
    this.spinner.show()

    if(!this.user)
    {
      const request = new UserRegisterRequest({ ...this.form.value, RoleIds: [this.form.value.RoleIds] })

      this.userService.register(request)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe({
        next: () => {
          this.closeModal();
          this.saveEmitter.emit();
          this.toastr.success("Usuário registrado com sucesso")
        }
      });
    }
    else {
      const request = new UserEditRequest({ ...this.form.value, RoleIds: [this.form.value.RoleIds] })

      this.userService.edit(this.user.Id, request)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe({
        next: () => {
          this.toastr.success("Usuário editado com sucesso")
          this.closeModal();
          this.saveEmitter.emit();
        }
      });
    }

  }

}
