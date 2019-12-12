DOSSEG
.MODEL SMALL
.STACK 100h
.486
.DATA

;======Declaration of variables =======
	A	dd	00h
	B	dd	00h
	C	dd	00h
	lb1	dw	0h
	lb2	dw	0h
	buf_if	dw	0h
	buf	dd	0
	rc	dw	0
;=======Data for Put() functions=======

	MSign	db	'+','$'
	X_Str	db	12 dup (0)
	ten	dw	10
	X1	dw	0h
	MX1	db	13,10,'>> $'
	per	db	10,13,'$'
	line0	db	'A: ','$'
.CODE
START:
	mov ax,@data
	mov ds,ax
	finit

	mov word ptr buf,01h
	fild buf
	fistp A
	call ltNot
	mov word ptr buf,01h
	fild buf
	fistp A
	lea dx,line0
	mov ah,09
	int 21h
	fild A
	fistp buf
	call output
;======================================
MOV AH,4Ch
INT 21h
;===Output procedure Put()=============
output PROC
	push ax
	push bx
	push cx
	push dx
	push di
	push si
	;saveregisters
	cmp buf,0
	je exit_0
	mov cl,byte ptr buf+3
	and cl,80h
	je m6
	fild buf
	fchs
	fistp buf
	mov MSign,'-'
M6:	mov cx,10
	mov di,0
O_1:	ffree st(0)
	ffree st(1)
	fild ten
	fild buf
	fprem
	fistp X1
	mov dl,byte ptr X1
	add dl,30h
	mov X_Str[di],dl
	inc di
	fild buf
	fxch st(1)
	fdiv
	fild X1
	fild ten
	fdiv
	fsub
	frndint
	fistp buf
	loop O_1
	cmp MSign,'+'
	je O_3
	mov dl,MSign
	mov ah,02
	int 21h
O_3:	inc di
	mov cx,12
O_2:	mov dl,X_Str[di]
	cmp dl,31h
	jge O_4
	dec di
	loop O_2
	jmp O_5
O_4:	mov dl,X_Str[di]
	mov ah,02h
	int 21h
	dec di
	loop O_4
O_5:	mov MSign,'+'
	jmp exit1
exit_0:	mov dl,30h
	mov ah,02
	int 21h	;restore registers
exit1:	pop si
	pop di
	pop dx
	pop cx
	pop bx
	pop ax
ret
output ENDP
;===Procedure mod_====================
	mod_ PROC
	fistp lb1
	fistp lb2
	fild lb1
	fild lb2
	fprem
	ret
mod_ ENDP
;===Procedure ltAnd====================
ltAnd PROC
	push ax
	push dx
	pushf

	fistp lb1
	fist lb2
	mov ax,lb1
	cmp ax,0

	jnz true_and1
	jz false_and
	true_and1:

	mov ax,lb2
	cmp ax,0
	jnz true_and

	false_and:
	fldz 
	jmp l_and

	true_and:
	fld1
	l_and: 
	popf
	pop dx
	pop ax
	ret
ltAnd ENDP


	;===Procedure ltOr======================
ltOr PROC
	push ax
	push dx
	pushf
	fistp lb1
	fist lb2
	mov ax,lb1
	cmp ax,0
	jnz true_or
	mov ax,lb2
	cmp ax,0
	jnz true_or
	fldz
	jmp l_or

	true_or:
	fld1

	l_or:
	popf
	pop dx
	pop ax
	ret
ltOr ENDP
;===Procedure ltNot====================
ltNot PROC
	push ax
	pushf
	fistp lb1
	mov ax,lb1
	cmp ax,0
	jne is_true
	fld1
	jmp l_not
is_true:
	fldz
l_not:
	popf
	pop ax
	ret
ltNot ENDP
;===Procedure eq_======================
eq_ PROC
	push ax
	push dx
	pushf
	fistp lb1
	fistp lb2
	mov ax,lb1
	mov dx,lb2
	cmp ax,dx
	jne not_eq
	fld1
	jmp l_eq
not_eq: fldz
l_eq:	popf
	pop dx
	pop ax
ret
eq_ ENDP
;===Procedure ltGreate======================
ltGreate PROC
	push ax
	push dx
	pushf
	fistp lb1
	fistp lb2
	mov ax,lb1
	mov dx,lb2
	cmp dx,ax
	jl lov
	fld1
	jmp l_ge
lov:	fldz
l_ge:	popf
	pop dx
	pop ax
	ret
ltGreate ENDP
;===Procedure ltLess======================
ltLess PROC
	push ax
	push dx
	pushf
	fistp lb1
	fistp lb2
	mov ax, lb1
	mov dx, lb2
	cmp dx,ax
	jg gr
lo:
	fld1
	jmp l_le
gr:
	fldz
	l_le:
	popf
	pop dx
	pop ax
	ret
ltLess ENDP

;======================================
END START