import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { LoginRequest } from '../../models/requests/login.request';
import { UsersService } from '../../services/users.service';
import { PasswordValidator } from '../../validators/password.validator';
import { PasswordMatchValidator } from '../../validators/password-match.validator';
import { UserResetPasswordRequest } from '../../models/requests/user-reset-password.request';
import { FormInvalidMessageComponent } from "../../../shared/components/form-invalid-message/form-invalid-message.component";

@Component({
  selector: 'app-reset-password',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FormInvalidMessageComponent
],
  templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent  implements OnInit{
  constructor(
    private fb: FormBuilder,
    private usersService : UsersService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
  ) {
    this.form = this.fb.group({
      Password: ['', [Validators.required, PasswordValidator]],
      CheckPassword: ['', Validators.required]
    }, {validators: PasswordMatchValidator('Password', 'CheckPassword')});
  }

  form: FormGroup;
  email: string;
  token: string;
  passwordInputType = "password"

  ngOnInit(): void {
    const params = this.route.snapshot.queryParams;

    this.email = params['email'];
    this.token = params['token'];
  }

  send() {
    this.spinner.show();

    const request = new UserResetPasswordRequest({... this.form.value, Email: this.email, Token: this.token});

    this.usersService.resetPassword(request)
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.toastr.success("Senha alterada com sucesso");
        this.router.navigateByUrl('/login');
      }
    });
  }
}
