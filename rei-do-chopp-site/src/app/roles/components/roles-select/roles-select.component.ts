import { AfterViewInit, Component, ElementRef, EventEmitter, forwardRef, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, NgControl, ReactiveFormsModule } from '@angular/forms';
import { RolesService } from '../../services/roles.service';
import { RoleResponse } from '../../models/responses/role.response';
import { CommonModule } from '@angular/common';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { debounceTime, map, Subject } from 'rxjs';
import { RoleListRequest } from '../../models/requests/role-list.request';

@Component({
  selector: 'app-roles-select',
  imports: [CommonModule, NgSelectModule, FormsModule],
  templateUrl: './roles-select.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => RolesSelectComponent),
      multi: true
    }
  ]
})
export class RolesSelectComponent implements ControlValueAccessor, OnInit {
  @ViewChild('roleSelect') roleSelectRef: NgSelectComponent;
  @Output('change') changeEmmiter = new EventEmitter<RoleResponse>();

  value: number = null;
  onChange: any = () => { };
  onTouched: any = () => { };
  loading = false;

  search$ = new Subject<string>();

  request = new RoleListRequest({});
  items: RoleResponse[];

  constructor(private rolesService: RolesService) { }
  ngOnInit(): void {
    this.search$.pipe(debounceTime(400))
      .subscribe(term => this.list(term));
    setTimeout(() => { this.list(null, this.value); })
  }

  public list(term?: string, id?: number) {
    this.loading = true;
    this.request = new RoleListRequest({ Id: id, Name: term });
    this.rolesService.list(this.request)
      .subscribe(response => {
        this.loading = false;
        this.items = response.Data;
      })
  }

  writeValue(obj: any): void {
    this.value = obj;
  }

  getfocused() {
    this.roleSelectRef?.focus();
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  };

  emmitChange(role: RoleResponse) {
    this.changeEmmiter.emit(role);
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {

  }

  updateValue(id: number) {
    this.value = id;
    if (!id) {
      this.list();
    }
    this.onChange(id);
    this.onTouched();

  }
}
