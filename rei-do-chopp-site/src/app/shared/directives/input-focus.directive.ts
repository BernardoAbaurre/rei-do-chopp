import { AfterViewInit, Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appInputFocus]'
})
export class InputFocusDirective implements AfterViewInit{

  constructor(private el: ElementRef<HTMLInputElement>) {}

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.el.nativeElement.focus();
    });
  }
}
