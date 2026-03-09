import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { SettingsService } from '../../../services/settings.service';
import Swal from 'sweetalert2';
import { SettingsAuthRequest } from '../../../models/settings-auth.request';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { FormInvalidMessageComponent } from "../../../../shared/components/form-invalid-message/form-invalid-message.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-reset-base-settings',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    FormInvalidMessageComponent
],
  templateUrl: './reset-base-settings.component.html',
  styleUrl: './reset-base-settings.component.css'
})
export class ResetBaseSettingsComponent implements OnInit {
  form: FormGroup;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly settingsService: SettingsService,
    private readonly spinner: NgxSpinnerService,
    private readonly toastr: ToastrService,
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      'ResetMode': ['stock'],
      'Password': ['', [Validators.required]]
    })
  }

  public askReset() {
    const text = this.form.get("ResetMode").value == 'base' ? 'deletar toda a base de dados?' : 'deletar o histórico de operações e zerar o estoque?'

    const title = this.form.get("ResetMode").value == 'base' ?
      "Todos os produtos e histórico de vendas e reabastecimentos serão apagados"
      : "Todos o histórico de vendas e reabastecimentos será apagado e o estoque será zerado";


    Swal.fire({
      title: 'Tem certeza que deseja ' + text,
      text: title,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não',
      allowEscapeKey: false,
      allowOutsideClick: false,
    }).then(response => {
      if (response.isConfirmed) {
        this.reset()
      }
    })
  }

  public reset()
  {

    const request = new SettingsAuthRequest({'Password': this.form.get("Password").value});

    const func = this.form.get("ResetMode").value == 'base' ? this.settingsService.resetBase(request) : this.settingsService.resetStock(request);

    this.spinner.show();

    func.pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.toastr.success("Operação concluída com sucesso");
        this.router.navigateByUrl('/hub');
      }
    });
  }

}
