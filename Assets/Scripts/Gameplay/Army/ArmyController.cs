using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class ArmyController : MonoBehaviour
{
    protected Army _armyData; //protected для доступа из playerarmycontroller и тд., приват не вышло бы
    [SerializeField] protected GameObject _samuraiObject;
    protected SpriteRenderer _samuraiSprite;
    protected Animator _samuraiAnim;
    protected Transform _currentDestination;
    protected bool _isMoving;
    protected float _moveSpeed;

    [SerializeField] private TextMesh _armySizeText;

    public Army ArmyData => _armyData;
    public event Action<Village, Army> OnVillageSteped;

    public void Init(Army armyData)
    {
        _armyData = armyData;

        UpdateSizeUI();

        _armyData.OnArmyChanged += UpdateSizeUI;
    }

    protected void InitializeComponents()
    {
        if (_samuraiObject != null)
        {
            _samuraiSprite = _samuraiObject.GetComponent<SpriteRenderer>();
            _samuraiAnim = _samuraiObject.GetComponent<Animator>();
        }
    }

    public void UpdateSizeUI()
    {
        if (_armySizeText != null && _armyData != null)
        {
            _armySizeText.text = _armyData.ArmyCount.ToString();
        }
    }

    protected void MoveToCoordinates(Transform _target)
    {
        _currentDestination = _target;
        _isMoving = true;
    }
    protected void MoveToCoordinates(Vector2 _target)
    {
        GameObject newObj = new GameObject("Point");
        newObj.transform.position = _target;

        _currentDestination = newObj.transform;
        _isMoving = true;
    }
    protected void MoveToCoordinates(Village _target)
    {
        _currentDestination = _target.transform;
        _isMoving = true;
    }

    protected virtual void FixedUpdate()
    {
        if (_isMoving)
        {
            if (_samuraiAnim != null)
            {
                _samuraiAnim.SetBool("Moving", true);
            }

            SpriteFlip();

            transform.position = Vector2.MoveTowards(transform.position, _currentDestination.position, _moveSpeed * Time.deltaTime * 10);

            if (new Vector2(transform.position.x, transform.position.y) == new Vector2(_currentDestination.position.x, _currentDestination.position.y))
            {
                StopMoving();
            }
        }
    }

    private void SpriteFlip()
    {
        if (_currentDestination.position.x > transform.position.x)
        {
            _samuraiSprite.flipX = false;
        }
        else
        {
            _samuraiSprite.flipX = true;
        }
    }

    protected virtual void StopMoving()
    {
        if (_currentDestination?.GetComponent<Village>())
        {
            OnVillageSteped.Invoke(_currentDestination.GetComponent<Village>(), ArmyData);
        }

        CustomStopMovingLogic();

        _isMoving = false;
        _currentDestination = null;

        _samuraiAnim.SetBool("Moving", false);
    }

    protected virtual void CustomStopMovingLogic() { }

}
