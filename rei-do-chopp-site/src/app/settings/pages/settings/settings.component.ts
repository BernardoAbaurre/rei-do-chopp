import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ResetBaseSettingsComponent } from "../components/reset-base-settings/reset-base-settings.component";

@Component({
  selector: 'app-settings',
  imports: [ResetBaseSettingsComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

}
