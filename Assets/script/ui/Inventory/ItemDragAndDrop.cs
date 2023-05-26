using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemDragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Image image;
    public Text text;

    private Image originalImage;
    //private string originalText;

    private Canvas canvas;
    private int siblingIndex;

    private Transform originalParent;
    private Vector3 originalPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the original parent and position
        originalParent = transform.parent;
        originalPosition = transform.position;

        // Move the item to the top of the sibling hierarchy
        transform.SetAsLastSibling();

        // Disable raycasting on the image during drag
        image.raycastTarget = false;

        // Store the original text
        //originalText = text.text;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerCurrentRaycast.gameObject;
        ItemDragAndDrop otherItem = droppedObject.GetComponent<ItemDragAndDrop>();

        if (otherItem != null)
        {
            // Swap the image and text values
            Sprite tempImage = image.sprite;
            string tempText = text.text;

            image.sprite = otherItem.originalImage.sprite;
            //text.text = otherItem.originalText;
            text.text = otherItem.text.text;

            otherItem.image.sprite = tempImage;
            otherItem.text.text = tempText;
        }

        // Reset the position of the dragged item
        transform.position = originalPosition;

        // Restore the parent and sibling index
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(siblingIndex);

        // Enable raycasting on the image after drag
        image.raycastTarget = true;
    }

    private void Awake()
    {
        // Store the original image and text values
        originalImage = image;
        //originalText = text.text;

        // Get the Canvas component of the parent
        canvas = GetComponentInParent<Canvas>();

        // Store the initial sibling index
        siblingIndex = transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        // Move the item to the top of the sibling hierarchy when enabled
        transform.SetAsLastSibling();
    }
}
