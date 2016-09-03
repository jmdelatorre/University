// sacado de https://gist.github.com/mycodeschool/7429492#file-doublylinkedlist-c

/* Doubly Linked List implementation */
#include<stdio.h>
#include<stdlib.h>

struct Node  {
	int data;
	struct Node* next;
	struct Node* prev;
};

struct Node* head; // global variable - pointer to head node.

//Creates a new Node and returns pointer to it.
struct Node* GetNewNode(int x) {
	struct Node* newNode
		= (struct Node*)malloc(sizeof(struct Node));
	newNode->data = x;
	newNode->prev = NULL;
	newNode->next = NULL;
	return newNode;
}


//Inserts a Node at tail of Doubly linked list
void InsertAtTail(int x) {
	struct Node* temp = head;
	struct Node* newNode = GetNewNode(x);
	if(head == NULL) {
		head = newNode;
		return;
	}
	while(temp->next != NULL) temp = temp->next; // Go To last Node
	temp->next = newNode;
	newNode->prev = temp;
}


//Prints all elements in linked list in reverse traversal order.
void ReversePrint() {
	struct Node* temp = head;
	if(temp == NULL) return; // empty list, exit
	// Going to last Node
	while(temp->next != NULL) {
		temp = temp->next;
	}
	// Traversing backward using prev pointer
	while(temp != NULL) {
		if(temp->data / 64 == 0){
			printf("R %d \n",temp->data);
		}
		if(temp->data / 64 == 1){
			printf("L %d \n",temp->data - 64);
		}
		if(temp->data / 64 == 2){
			printf("U %d \n",temp->data- 2*64);
		}
		if(temp->data / 64 == 3){
			printf("D %d \n",temp->data- 3*64);
		}		temp = temp->prev;
	}
}
